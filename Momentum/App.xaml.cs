using System;
using System.IO;
using Momentum.Services;

namespace Momentum
{
    public partial class App : Application
    {
        // Base de datos local SQLite
        public static TaskDatabase Database { get; private set; }

        // Servicio API para sincronización
        public static TaskService ApiService { get; private set; }

        public App()
        {
            InitializeComponent();

            // 🔹 Obtener la ruta de la base de datos
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tasks.db");

            // 🔹 Inicializar la base de datos con la ruta correcta
            Database = new TaskDatabase(dbPath);
            ApiService = new TaskService();

            MainPage = new AppShell();
        }
    }
}
