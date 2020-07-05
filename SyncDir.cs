using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Forms;

namespace SyncFolders
{
    public class SyncDir
    {
        public enum DirectoryChangeEventType
        {
            OnChange,
            OnCreate,
            OnDelete,
            OnRename,
            OnFullSave
        }

        public string InputDir { get; set; }
        public string OutputDir { get; set; }

        public void WatchDirectory(bool realTime)
        {
            try
            {
                var timer = new System.Timers.Timer() { Interval = 300000, AutoReset = true};

                var watcher = new FileSystemWatcher
                {
                    Path = InputDir,
                    IncludeSubdirectories = true,
                    NotifyFilter = NotifyFilters.Attributes    | NotifyFilters.CreationTime |
                                   NotifyFilters.DirectoryName | NotifyFilters.FileName     | NotifyFilters.LastAccess |
                                   NotifyFilters.LastWrite     | NotifyFilters.Security     | NotifyFilters.Size,
                    Filter = "*.*"
                };

                if (!realTime)
                {
                    watcher.Changed += new FileSystemEventHandler(OnChanged);
                    watcher.Created += new FileSystemEventHandler(OnCreated);
                    watcher.Deleted += new FileSystemEventHandler(OnDeleted);
                    watcher.Renamed += new RenamedEventHandler(OnRename);
                    watcher.Error += new ErrorEventHandler(OnError);

                    watcher.EnableRaisingEvents = true;

                    return;
                }

                HandleError(null, true, "Atenção, feche qualquer programa que esteja fazendo operações de leitura neste diretório.");
                timer.Enabled = true;
                timer.Elapsed += OnFullSave;

                ChangeLabel("statusLabel", "Aguardando para sincronizar...", Color.DarkOrange);

            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message, "Erro ao monitorar o diretório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
                ChangeLabel("statusLabel", "Erro ao sincronizar", Color.Red);
            }

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
                        try
                        {
                            File.Copy(fullName, destinationFile, true);
                            Console.WriteLine("[MODIFICACAO] {0}, no local {1} foi modificado.", fileName, destinationFile);
                        }
                        catch (Exception e)
                        {
                            HandleError(e);
                        }

                    }
                    else
                    {
                        Console.WriteLine("[MODIFICACAO] Arquivo {0} não existe no diretório de origem.", fullName);
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
                            try
                            {
                                File.Copy(fullName, destinationFile);
                                Console.WriteLine("[CRIACAO] {0}, no local {1} foi criado.", fileName, destinationFile);
                            }
                            catch (Exception e)
                            {
                                HandleError(e);
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("[CRIACAO] Arquivo {0} não existe no diretório de origem.", fullName);
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
                            Console.WriteLine("[DELECAO] {0}, no local {1} foi deletado.", fileName, destinationFile);
                        }
                        catch (Exception e)
                        {
                            HandleError(e);
                        }

                    }
                    else
                    {
                        Console.WriteLine("[DELECAO] Arquivo {0} não existe no diretório de origem.", destinationFile);
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
                                Console.WriteLine(" {0} foi renomeado para {1}", destinationOldName, destinationNewName);
                            }
                            catch (Exception e)
                            {
                                HandleError(e);
                            }
                        }
                        else
                        {
                            Console.WriteLine("[RENOMEACAO] Arquivo {0} não existe no diretório de origem.", destinationOldName);
                        }

                    ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
                    ChangeLabel("statusLabel", "Está sincronizando", Color.Green);
                    break;
                }

                case DirectoryChangeEventType.OnFullSave:
                {
                    CopyDir.DirectoryCopy(InputDir, OutputDir, true, true);
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
            Console.WriteLine(" {0} foi renomeado para {1}", e.OldFullPath, e.FullPath);
            HandleChange(DirectoryChangeEventType.OnRename, "", "", e.OldName, e.OldFullPath, 
                                                                                e.Name, e.FullPath);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("{0}, no local {1} foi deletado.", e.Name, e.FullPath);
            HandleChange(DirectoryChangeEventType.OnDelete, e.Name, e.FullPath);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("{0}, no local {1} foi criado.", e.Name, e.FullPath);
            HandleChange(DirectoryChangeEventType.OnCreate, e.Name, e.FullPath);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("{0}, no local {1} foi modificado.", e.Name, e.FullPath);
            HandleChange(DirectoryChangeEventType.OnChange, e.Name, e.FullPath);
        }
        private void OnFullSave(object sender, ElapsedEventArgs e)
        {

            Console.WriteLine("Iniciando a cópia completa do diretório {0}...", InputDir);
            HandleChange(DirectoryChangeEventType.OnFullSave);
            Console.WriteLine("Diretório {0} copiado com sucesso.", InputDir);

            ChangeLabel("lastChangeLabel", DateTime.Now.ToShortTimeString(), Color.Green);
            ChangeLabel("statusLabel", "Está sincronizando", Color.Green);
        }

    }
}

