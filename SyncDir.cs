using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using Serilog;
using SyncFolders.Util;

namespace SyncFolders
{
    public class SyncDir
    {
        private readonly Logger _logger = new Logger();

        public enum DirectoryChangeEventType
        {
            OnChange,
            OnCreate,
            OnCreateDir,
            OnDelete,
            OnDeleteDir,
            OnRename,
            OnRenameDir,
            OnFullSave
        }

        public SyncDir()
        {
            _logger.InitializeLogger();
        }

        public string InputDir { get; set; }
        public string OutputDir { get; set; }

        public void WatchDirectory(bool realTime, long timeInMinutes = 0)
        {
            try
            {
                var timer = new System.Timers.Timer() 
                { 
                    Interval = MinutesToMilis(timeInMinutes) == 0 ? 60000 : MinutesToMilis(timeInMinutes), 
                    AutoReset = true
                };

                var watcher = new FileSystemWatcher
                {
                    Path = InputDir,
                    IncludeSubdirectories = true,
                    NotifyFilter = NotifyFilters.Attributes    | NotifyFilters.CreationTime |
                                   NotifyFilters.DirectoryName | NotifyFilters.FileName     | NotifyFilters.LastAccess |
                                   NotifyFilters.LastWrite     | NotifyFilters.Security     | NotifyFilters.Size,
                    Filter = "*.*"
                };

                if (realTime)
                {
                    watcher.Changed += new FileSystemEventHandler(OnChanged);
                    watcher.Created += new FileSystemEventHandler(OnCreated);
                    watcher.Deleted += new FileSystemEventHandler(OnDeleted);
                    watcher.Renamed += new RenamedEventHandler(OnRename);
                    watcher.Error += new ErrorEventHandler(OnError);

                    watcher.EnableRaisingEvents = true;
                    timer.Enabled = false;
                    return;
                }

                HandleError(null, true, "Atenção, feche qualquer programa que esteja fazendo operações de leitura neste diretório.");

                timer.Enabled = true;
                timer.Elapsed += OnFullSave;
                watcher.EnableRaisingEvents = false;

                ChangeLabel("statusLabel", "Aguardando para sincronizar...", Color.DarkOrange);
            }

            catch (IOException e)
            {
                Log.Error("Erro ao monitorar o diretório {0}", InputDir);

                MessageBox.Show(e.Message, "Erro ao monitorar o diretório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
                ChangeLabel("statusLabel", "Erro ao sincronizar", Color.Red);
            }

        }

        private void CreateFileDirectoryIfNotExists(string fileName)
        {
            var dirName = string.IsNullOrWhiteSpace(Path.GetDirectoryName(fileName))
                ? string.Empty 
                : Path.GetDirectoryName(fileName);

            var newDir = Path.Combine(OutputDir, dirName);

            if (!Directory.Exists(newDir))
                Directory.CreateDirectory(newDir);
        }

        private static long MinutesToMilis(long minutes)
        {
            return minutes * 60000;
        }

        private static void ChangeLabel(string labelId, string text, Color color)
        {
            var label = (Label)Application.OpenForms["MainForm"]
                ?.Controls
                .Find(labelId, false).FirstOrDefault();

            label?.Invoke((MethodInvoker)delegate
            {
                label.Text = text;
                label.ForeColor = color;
            });
        }

        private static void HandleError(Exception e, bool info = false, string message = "")
        {
            Log.Error(message);

            MessageBox.Show(info ? message : e.Message,
                "Erro ao monitorar o diretório", MessageBoxButtons.OK, info ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private void HandleChange(DirectoryChangeEventType changeType, string fileName = "", string fullName = "", 
                                                                        string oldName = "", string oldPath = "", 
                                                                        string newName = "", string newPath = "")
        {
            switch (changeType)
            {
                case DirectoryChangeEventType.OnChange:
                {
                    var destinationFile = Path.Combine(OutputDir, fileName);

                    if (File.Exists(fullName))
                    {
                        CreateFileDirectoryIfNotExists(fileName);

                        try
                        {
                            File.Copy(fullName, destinationFile, true);
                            Log.Information("[MODIFICAÇÃO] {0}, no local {1} foi modificado.", fileName, destinationFile);
                        }
                        catch (Exception e)
                        {
                            HandleError(e);
                        }

                    }
                    else
                    {
                        Log.Information("[MODIFICAÇÃO] Arquivo {0} não existe no diretório de origem.", fullName);
                    }

                    ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
                    ChangeLabel("statusLabel", "Está sincronizando", Color.Green);
                    break;
                }

                case DirectoryChangeEventType.OnCreate:
                {
                    var destinationFile = Path.Combine(OutputDir, fileName);

                        if (File.Exists(fullName))
                        {
                            CreateFileDirectoryIfNotExists(fileName);

                            try
                            {
                                File.Copy(fullName, destinationFile);
                                Log.Information("[CRIAÇÃO] {0}, no local {1} foi criado.", fileName, destinationFile);
                            }
                            catch (Exception e)
                            {
                                HandleError(e);
                            }
                            
                        }
                        else
                        {
                            Log.Information("[CRIAÇÃO] Arquivo {0} não existe no diretório de origem.", fullName);
                        }

                    ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
                    ChangeLabel("statusLabel", "Está sincronizando", Color.Green);
                    break;
                }

                case DirectoryChangeEventType.OnCreateDir:
                {
                    var destinationDir = Path.Combine(OutputDir, fileName);

                    if (!Directory.Exists(destinationDir))
                    {
                        try
                        {
                            Directory.CreateDirectory(destinationDir);
                            Log.Information("[CRIAÇÃO DE DIRETÓRIO] {0}, no local {1} foi criado.", fileName, destinationDir);
                        }
                        catch (Exception e)
                        {
                            HandleError(e);
                        }

                    }
                    else
                    {
                        Log.Information("[CRIAÇÃO DE DIRETÓRIO] Diretório {0} já existe.", destinationDir);
                    }

                    ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
                    ChangeLabel("statusLabel", "Está sincronizando", Color.Green);
                    break;
                }

                case DirectoryChangeEventType.OnDelete:
                {
                    var destinationFile = Path.Combine(OutputDir, fileName);

                    if (File.Exists(destinationFile))
                    {
                        try
                        {
                            File.Delete(destinationFile);
                            Log.Information("[DELEÇÃO] {0}, no local {1} foi deletado.", fileName, destinationFile);
                        }
                        catch (Exception e)
                        {
                            HandleError(e);
                        }

                    }
                    else
                    {
                        Log.Information("[DELEÇÃO] Arquivo {0} não existe no diretório de origem.", destinationFile);
                    }

                    ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
                    ChangeLabel("statusLabel", "Está sincronizando", Color.Green);
                    break;
                }

                case DirectoryChangeEventType.OnDeleteDir:
                {
                    var destinationDir = Path.Combine(OutputDir, fileName);

                    if (Directory.Exists(destinationDir))
                    {
                        try
                        {
                            var dirToDelete = new DirectoryInfo(destinationDir);
                            dirToDelete.Delete(true);
                            Log.Information("[DELEÇÃO DE DIRETÓRIO] {0}, no local {1} foi deletado.", fileName, destinationDir);
                        }
                        catch (Exception e)
                        {
                            HandleError(e);
                        }

                    }

                    else
                    {
                        Log.Information("[DELEÇÃO DE DIRETÓRIO] Diretório {0} não existe.", destinationDir);
                    }

                    ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
                    ChangeLabel("statusLabel", "Está sincronizando", Color.Green);
                    break;
                }

                case DirectoryChangeEventType.OnRename:
                {
                    var destinationOldName = Path.Combine(OutputDir, oldName);
                    var destinationNewName = Path.Combine(OutputDir, newName);

                        if (File.Exists(destinationOldName))
                        {
                            try
                            {
                                File.Move(destinationOldName, destinationNewName);
                                Log.Information(" {0} foi renomeado para {1}", destinationOldName, destinationNewName);
                            }
                            catch (Exception e)
                            {
                                HandleError(e);
                            }
                        }
                        else
                        {
                            Log.Information("[RENOMEAÇÃO] Arquivo {0} não existe no diretório de origem.", destinationOldName);
                        }

                    ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
                    ChangeLabel("statusLabel", "Está sincronizando", Color.Green);
                    break;
                }

                case DirectoryChangeEventType.OnRenameDir:
                {
                    var destinationOldName = Path.Combine(OutputDir, oldName);
                    var destinationNewName = Path.Combine(OutputDir, newName);

                    if (Directory.Exists(destinationOldName))
                    {
                        try
                        {
                            Directory.Move(destinationOldName, destinationNewName);
                            Log.Information(" {0} foi renomeado para {1}", destinationOldName, destinationNewName);
                        }
                        catch (Exception e)
                        {
                            HandleError(e);
                        }
                    }
                    else
                    {
                        Log.Information("[RENOMEAÇÃO DE DIRETÓRIO] Diretório {0} não existe no diretório de origem.", destinationOldName);
                    }

                    ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
                    ChangeLabel("statusLabel", "Está sincronizando", Color.Green);
                    break;
                }

                case DirectoryChangeEventType.OnFullSave:
                {
                    DirUtils.DirectoryCopy(InputDir, OutputDir, true, true);
                    break;
                }

                default:
                    HandleError(new ArgumentOutOfRangeException(nameof(changeType), changeType, null)); 
                    break;
            }
        }

        private static void OnError(object sender, ErrorEventArgs e)
        {
            HandleError(e.GetException());
        }

        private void OnRename(object sender, RenamedEventArgs e)
        {
            Log.Information(" {0} foi renomeado para {1}", e.OldFullPath, e.FullPath);

            HandleChange(
                !string.IsNullOrWhiteSpace(Path.GetExtension(e.OldFullPath))
                    ? DirectoryChangeEventType.OnRename
                    : DirectoryChangeEventType.OnRenameDir, "", "", e.OldName, e.OldFullPath,
                e.Name, e.FullPath
            );
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Log.Information("{0}, no local {1} foi deletado.", e.Name, e.FullPath);
            HandleChange(
                !string.IsNullOrWhiteSpace(Path.GetExtension(e.FullPath))
                ? DirectoryChangeEventType.OnDelete 
                : DirectoryChangeEventType.OnDeleteDir, e.Name, e.FullPath
            );
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Log.Information("{0}, no local {1} foi criado.", e.Name, e.FullPath);
            HandleChange(
                !string.IsNullOrWhiteSpace(Path.GetExtension(e.FullPath)) 
                ? DirectoryChangeEventType.OnCreate 
                : DirectoryChangeEventType.OnCreateDir, e.Name, e.FullPath
            );
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Log.Information("{0}, no local {1} foi modificado.", e.Name, e.FullPath);
            HandleChange(DirectoryChangeEventType.OnChange, e.Name, e.FullPath);
        }
        private void OnFullSave(object sender, ElapsedEventArgs e)
        {

            Log.Information("Iniciando a cópia completa do diretório {0}...", InputDir);
            HandleChange(DirectoryChangeEventType.OnFullSave);
            Log.Information("Diretório {0} copiado com sucesso.", InputDir);

            ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
            ChangeLabel("statusLabel", "Está sincronizando", Color.Green);
        }

    }
}

