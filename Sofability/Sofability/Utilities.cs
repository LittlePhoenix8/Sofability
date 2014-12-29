using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofability
{
    public static class Utilities
    {
        /// <summary>
        /// Navega a otra página XAML.
        /// </summary>
        /// <param name="page">El nombre de la página a navegar.</param>
        /// <param name="parameters">Lista par de string pasados como parámetros a la página.</param>
        public static void Navigate(this PhoneApplicationPage pap, string page, params string[] parameters)
        {
            var nParams = parameters.Length;
            if (nParams % 2 != 0)
                throw new ArgumentException("Los parámetros pasados a la página deben ser pares: atributo-valor.");
            var sb = new StringBuilder("/" + page + ".xaml?");

            if (nParams > 0)
            {
                for (int i = 0; i < nParams; i += 2)
                {
                    sb.Append(Uri.EscapeDataString(parameters[i]));
                    sb.Append("=");
                    sb.Append(Uri.EscapeDataString(parameters[i + 1]));
                    sb.Append("&");
                }
            }

            pap.NavigationService.Navigate(new Uri(sb.ToString(0, sb.Length - 1), UriKind.Relative));
        }

        /// <summary>
        /// Quita las últimas páginas de navegación. Por defecto no deja ninguna.
        /// </summary>
        /// <param name="entriesCount">Número máximo de páginas a sacar.</param>
        public static void RemoveNavigationEntries(this PhoneApplicationPage pap, int entriesCount = 0)
        {
            if (entriesCount == 0)
                while (pap.NavigationService.BackStack.Any())
                    pap.NavigationService.RemoveBackEntry();
            else
            {
                entriesCount = Math.Min(entriesCount, pap.NavigationService.BackStack.Count());
                for (var i = 0; i < entriesCount; i++)
                    pap.NavigationService.RemoveBackEntry();
            }
        }
    }
}
