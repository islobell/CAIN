using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

namespace CAIN
{
    /// <summary>
    ///    Clase para comparar información musical.
    /// </summary>
    public static class MetadataComparer
    {
        /// <summary>
        ///    Método para comparar dos títulos de canciones.
        /// </summary>  
        /// <param name="title1">
        ///    El primer título.
        /// </param>   
        /// <param name="title2">
        ///    El segundo título.
        /// </param>  
        /// <returns>
        ///    True, si los títulos son iguales. False, sino.
        /// </returns>
        public static bool CompareTitles(string title1, string title2)
        {
            /* 
             * Desestimamos los títulos que incluyan los siguientes patrones: 
             * que empiezan por: '(video', '(original', '(deluxe', (version', '(mix' 
             * que terminan por: 'video)', 'original)', 'edition)', 'version)', 'mix)' 
             */

            string pattern = @"(\(video|video\)|\(deluxe|edition\)|\(version|version\)|\(behind|scenes\)|\(mix|mix\))";
            //string pattern = @"(\(video|video\)|\(original|original\)|\(version|version\)|\(mix|mix\))";

            if (Regex.Match(title1.ToLower(), pattern).Success ||
                Regex.Match(title2.ToLower(), pattern).Success)
                return false;

            return MetadataComparer.StringsAreEqual(title1, title2);       
        }

        /// <summary>
        ///    Método para comparar dos listas de artistas.
        /// </summary>  
        /// <param name="artist1">
        ///    La primera lista de artistas.
        /// </param>  
        /// <param name="artist2"> 
        ///    La segunda lista de artistas.
        /// </param>
        /// <returns>
        ///    True, si ambas listas son iguales. False, sino.
        /// </returns>
        public static bool CompareArtists(List<string> artist1, List<string> artist2)
        {
            /* Analizamos las listas de artistas, por si un elemento contiene varios artistas */

            List<string> artistsA = MetadataComparer.SplitArtists(artist1);
            List<string> artistsB = MetadataComparer.SplitArtists(artist2);

            /* Obtenemos la lista con menos elementos, para asegurarnos de que al recorrer las listas no se produce una excepción */
            
            int lengthA = artistsA.Count < artistsB.Count ? artistsA.Count : artistsB.Count;
            int lengthB = artistsA.Count < artistsB.Count ? artistsB.Count : artistsA.Count;
            List<string> listA = artistsA.Count < artistsB.Count ? artistsA : artistsB;
            List<string> listB = artistsA.Count < artistsB.Count ? artistsB : artistsA;

            /* Comparamos las listas para ver si hay coincidencias */

            int count = 0;

            for (int i = 0; i < lengthA; i++)
            {
                string str1 = listA[i];

                for (int j = 0; j < lengthB; j++)
                {
                    string str2 = listB[j];

                    /* Comprobamos si una cadena es una abreviatura de la otra */

                    if (MetadataComparer.StringsAreEqual(str1, str2) || MetadataComparer.IsAbbreviation(str1, str2))
                        count++;
                }
            }

            /* Si coincide al menos un artista, lo damos por bueno */

            return count > 0;
        }

        /// <summary>
        ///    Método para comparar dos álbumes.
        /// </summary>  
        /// <param name="album1">
        ///    El primer álbum.
        /// </param> 
        /// <param name="album2">
        ///    El segundo álbum.
        /// </param> 
        /// <param name="tolerance">
        ///    La tolerancia permitida durante la comparación.
        /// </param> 
        /// <param name="reliability">
        ///    La fiabilidad de la comparación.
        /// </param>
        /// <returns>
        ///    True, si ambos álbumes son iguales. False, sino.
        /// </returns>
        public static bool CompareAlbums(string album1, string album2, float tolerance, out float reliability)
        {
            return MetadataComparer.StringsAreEqual(album1, album2, tolerance, out reliability); 
        }

        /// <summary>
        ///    Método para buscar artistas dentro de los elementos de la lista de artistas.
        /// </summary>  
        /// <param name="artists">
        ///    La lista de artistas.
        /// </param> 
        /// <returns>
        ///    La lista de artistas resultante.
        /// </returns>
        private static List<string> SplitArtists(List<string> artists)
        {
            List<string> result = new List<string>();

            /* Recorremos la lista de artistas */

            for (int i = 0; i < artists.Count; i++)
            {
                string artist = artists[i];

                /* Pasamos la cadena a minúsculas */

                artist = artist.ToLower();

                /* Descomponemos el artista buscando patrones como: "Ft.", "&", "and" o "/" */

                string[] separators = new string[] { " ft. ", " & ", " and ", "/" };

                string[] items = artist.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                /* Quitamos los espacios sobrantes */

                for (int j = 0; j < items.Length; j++)
                    items[j] = items[j].Trim();

                /* Añadimos los artistas encontrados a la lista */
                    
                result.AddRange(items);
            }

            return result;
        }

        /// <summary>
        ///    Método para comparar si un artista es abreviatura de otro.
        /// </summary>  
        /// <param name="str1">
        ///    El primer artista.
        /// </param> 
        /// <param name="str2">
        ///    El segundo artista.
        /// </param> 
        /// <returns>
        ///    True, si un artista es abreviatura del otro artista. False, sino.
        /// </returns>
        private static bool IsAbbreviation(string str1, string str2)
        {
            /* Separamos el texto en palabras */

            string[] words1 = str1.Split(' ');
            string[] words2 = str2.Split(' ');

            /* Si ambos artistas sólo tiene una palabra, no continuamos */

            if (words1.Length != 1 && words2.Length != 1)
                return false;

            /* Obtenemos la lista de palabras más corta (suponemos que es la abreviatura). La otra lista será las palabras a comparar */

            string abr = words1.Length < words2.Length ? words1[0] : words2[0];
            string[] wordsToCompare = words1.Length < words2.Length ? words2 : words1;

            /* Si la longitud de las listas de palabras no coincide, no continuamos */
            
            if (abr.Length != wordsToCompare.Length)
                return false;

            /* Comparamos la abreviatura con la letra inicial de cada uno de los elementos de la lista de palabras a comparar */

            int sum = 0;

            for (int j = 0; j < abr.Length; j++)
            {
                if (abr[j] == wordsToCompare[j][0])
                    sum++;
            }

            /* Si el número de coincidencias es igual al número de letras que contiene la abreviatura, lo damos por bueno */

            return sum == abr.Length;
        }

        /// <summary>
        ///    Método general para la comparación de dos cadenas de texto.
        /// </summary>  
        /// <param name="str1">
        ///    La primera cadena de texto.
        /// </param> 
        /// <param name="str2">    
        ///    La segunda cadena de texto.
        /// </param> 
        /// <returns>
        ///    True, si las dos cadenas de texto son iguales. False, sino.
        /// </returns>
        public static bool StringsAreEqual(string str1, string str2)
        {
            float reliability;
            return StringsAreEqual(str1, str2, 0.3f, out reliability);
        }

        /// <summary>
        ///    Método específico para la comparación de dos cadenas de texto.
        /// </summary>  
        /// <param name="str1">
        ///    La primera cadena de texto.
        /// </param> 
        /// <param name="str2">    
        ///    La segunda cadena de texto.
        /// </param>  
        /// <param name="tolerance">
        ///    La tolerancia permitida durante la comparación.
        /// </param> 
        /// <param name="reliability">
        ///    La fiabilidad de la comparación.
        /// </param>
        /// <returns>
        ///    True, si las dos cadenas de texto son iguales. False, sino.
        /// </returns>
        static public bool StringsAreEqual(string str1, string str2, float tolerance, out float reliability)
        {
            reliability = 0.0f;

            /* Si alguna de las cadenas es nula o está vacía, no continuamos */

            if (String.IsNullOrEmpty(str1)) return false;
            if (String.IsNullOrEmpty(str2)) return false;

            /* Pasamos las cadenas a minúsculas y quitamos los caracteres no válidos */

            str1 = Utils.RemoveInvalidChars(str1.ToLower());
            str2 = Utils.RemoveInvalidChars(str2.ToLower());

            /* Quitamos la parte final de la cadena que empieza por '-' ó '(' */

            Match m = null;
            m = Regex.Match(str1, @"[:\(-]");
            if (m.Success) str1 = str1.Substring(0, m.Index);
            m = Regex.Match(str2, @"[:\(-]");
            if (m.Success) str2 = str2.Substring(0, m.Index);

            /* Quitamos los espacios sobrantes de las cadenas */

            string strA = str1.Trim();
            string strB = str2.Trim();

            /* Calculamos la distancia Levenshtein */

            float dist = Utils.LevenshteinDistance(strA, strB);

            /* Establecemos el nivel de tolerancia */

            int length = str1.Length < str2.Length ? str1.Length : str2.Length;

            int limit = (int) Math.Ceiling(length * tolerance);

            /* Calculamos la fiabilidad del resultado */

            length = str1.Length > str2.Length ? str1.Length : str2.Length;

            reliability = dist != -1 ? 1.0f - (dist / length) : 0.0f;

            return dist != -1 && dist <= limit ? true : false;
        }
    }
}
