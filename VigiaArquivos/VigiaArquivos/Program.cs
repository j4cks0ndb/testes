using System;
using System.IO;

namespace VigiaArquivos
{
    /// <summary>
    /// Monitoramento de Arquivos em Pastas Windows
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Monitorador de arquivos";
            Console.WriteLine("****************************************");
            Console.WriteLine("Monitorando os arquivos .txt");

            // Estabelece a rota dos arquivos para monitorar
            FileSystemWatcher watcher = new FileSystemWatcher();
            try
            {
                watcher.Path = @"C:\Temp";
            }
            catch(Exception e)
            {
                Console.WriteLine("\nOcorreu um erro: " + e.Message);
                return;
            }

            // Define o que deverá ser monitorado nos arquivos
            watcher.NotifyFilter = NotifyFilters.LastWrite
                | NotifyFilters.FileName
                | NotifyFilters.DirectoryName;
                //NotifyFilters.LastAccess
                //| NotifyFilters.Attributes
                //| NotifyFilters.CreationTime
                //| NotifyFilters.Security
                //| NotifyFilters.Size
                
            // Somente vigiará os arquivos de texto
            watcher.Filter = "*.txt";

            // Manipuladores (delegates) de eventos
            watcher.Changed += new FileSystemEventHandler(onChanged);
            watcher.Created += new FileSystemEventHandler(onChanged);
            watcher.Deleted += new FileSystemEventHandler(onDeleted);
            watcher.Renamed += new RenamedEventHandler(onRenamed);

            // Começa a vigiar o diretório
            watcher.EnableRaisingEvents = true;

            // Aguarda pelo término do programa por iniciativa do usuário
            Console.WriteLine("Pressione 'q' para sair da aplicação");
            while(Console.Read() != 'q');
        }

        /// <summary>
        /// Evento disparado quando o arquivo é alterado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void onChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Arquivo: {0} \t Tipo de Alteração: {1}", e.Name, e.ChangeType);
        }

        /// <summary>
        /// Evento disparado quando o arquivo é removido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void onDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("O arquivo {0} foi excluído.", e.Name);
        }

        /// <summary>
        /// Evento disparado quando o arquivo é renomeado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void onRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("O arquivo {0} foi renomeado para: {1}", e.OldName, e.Name);
        }
    }
}
