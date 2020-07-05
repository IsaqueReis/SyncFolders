using System.IO;

namespace SyncFolders
{
    public class CopyDir
    {
        /// <summary>
        /// Pedaço de código retirado de: https://docs.microsoft.com/pt-br/dotnet/standard/io/how-to-copy-directories
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"> caso queira que os subdiretórios sejam copiados</param>
        /// <param name="overwrite"> caso queira que os arquivos sejam sobrescritos</param>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs = false, bool overwrite = false)
        {
            var dir = new DirectoryInfo(sourceDirName); //pega os subdiretórios do diretório especificado

            if(!dir.Exists)
                throw new DirectoryNotFoundException(
                    $"O diretório {sourceDirName} não existe ou não pode ser encontrado!");

            var dirs = dir.GetDirectories();

            //se o diretório de destino não existir, crie-o
            if (!Directory.Exists(destDirName))
                Directory.CreateDirectory(destDirName);

            //Pegue os arquivos do diretório e os copie para o novo local
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var tmpPath = Path.Combine(destDirName, file.Name);
                if (file.Extension == ".lock") //arquivo aberto por outro programa
                    continue;
                file.CopyTo(tmpPath, overwrite);
            }

            //Se estiver copiando subdiretórios, copie seu conteúdo para o novo local
            if (copySubDirs)
            {
                foreach (var subdir in dirs)
                {
                    var tmpPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tmpPath, copySubDirs, overwrite);
                }
            }
        }
    }
}
