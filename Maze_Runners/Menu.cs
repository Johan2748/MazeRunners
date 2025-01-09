using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console; 

namespace Maze_Runners
{
    class Menu
    {
        // Titula y opciones del menu
        private string title;
        private string[] options;

        // Variable q almacena la opcion seleccionada
        public string SelectedOption { get; private set; }

        
        public Menu(string title, string[] options)
        {
            this.title = title;
            this.options = options;
        }

        // Imprime el logo de MAZE RUNNERS
        public static void PrintLogo()
        {
            AnsiConsole.Write(new FigletText("MAZE RUNNERS").Color(Color.DarkMagenta_1));
        }

        // Muestra el menu y sus distintas opciones, y se mantiene en espera de q se seleccione una
        public void SetMenu()
        {
            SelectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[green] {title} [/]")
                    .AddChoices(options
                    ));

            Console.Clear();
        }




        
    }
}
