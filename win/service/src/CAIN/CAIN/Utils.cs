using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;

namespace CAIN
{
    /// <summary>
    /// Clase que contiene métodos útiles que se usan en toda la aplicación
    /// </summary>
    class Utils
    {
        public static ConsoleKeyInfo ReadConsoleKey(bool showKey, params ConsoleKey[] keys)
        {
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(!showKey);

                if (showKey && !keys.Contains(key.Key))
                    Console.SetCursorPosition(Console.CursorLeft > 0 ? Console.CursorLeft - 1 : 0, Console.CursorTop);
            }
            while (!keys.Contains(key.Key));

            return key;
        }

        public static void DeleteConsoleLines(int lines)
        {
            if (lines < 1) return;

            int currentLineCursor = Console.CursorTop;

            for (int i = 0; i < lines; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new String(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor - i);
            }
        }

        public static bool GetIndexes(string str, out int i, out int j, out int k)
        {
            i = -1;
            j = -1;
            k = -1;

            string[] indexes = str.Split(new char[] { ';' });

            Debug.Assert(indexes.Length == 3);
            if (indexes.Length != 3) return false;

            i = Convert.ToInt32(indexes[0]);
            j = Convert.ToInt32(indexes[1]);
            k = Convert.ToInt32(indexes[2]); 
            
            return true;
        }

        public static bool FloatEquals(float num1, float num2)
        {
            return Math.Abs(num1 - num2) <= 0.000001f;
        }

        public static DateTime GetDateTime(string date)
        {
            DateTime dt;
            bool ok = DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            if (!ok)
            {
                string[] items = date.Split(new char[]{'-'}, StringSplitOptions.RemoveEmptyEntries);
                if (items.Length == 1)
                    dt = new DateTime(Int32.Parse(items[0]), 12, 31);
                else if (items.Length == 2)
                    dt = new DateTime(Int32.Parse(items[0]), Int32.Parse(items[1]), 28);
                else
                    dt = DateTime.MaxValue;
            }

            return dt;
        }

        public static MusicBrainz.Data.Release GetMusicBrainzDataByIdList(List<string> releaseGroupIds)
        {
            MusicBrainz.Data.Release release = null;

            int size = 50;
            int times = releaseGroupIds.Count / size;
            times += releaseGroupIds.Count % size > 0 ? 1 : 0;

            for (int i = 0; i < times; i++)
            {
                StringBuilder str = new StringBuilder("rgid: ");
                str.Append(String.Join(" OR rgid: ", releaseGroupIds.Skip(i * 50).Take(size).ToList()));

                MusicBrainz.Data.Release rel = Utils.GetMusicBrainzData(str.ToString());

                if (release == null)
                    release = rel;
                else
                {
                    release.Data.AddRange(rel.Data);
                    release.Count += rel.Data.Count;
                }
            }

            /* 
             * Si hay datos, filtramos la información de la siguiente manera:
             * 1.- Cogemos sólo los registros cuyo Id sea igual al de los álbumes que estamos buscando.
             * 2.- Ordenamos el conjunto de registros por la fecha de más antiguo a más actual
             */

            if (release != null && release.Data.Count > 0)
                release.Data = release.Data
                    .Where(item => releaseGroupIds.Contains(item.Releasegroup.Id) && item.Status == "Official" && item.Releasegroup.Primarytype == "Album")
                    .OrderBy(item => Utils.GetDateTime(item.Date)).GroupBy(item => item.Releasegroup.Id).Select(item => item.First()).ToList();    

            return release;
        }

        public static MusicBrainz.Data.Release GetMusicBrainzDataById(string releaseGroupId)
        {
            MusicBrainz.Data.Release release = Utils.GetMusicBrainzData("rgid: " + releaseGroupId); 

            /* 
             * Si hay más de un registro, filtramos la información de la siguiente manera:
             * 1.- Cogemos sólo los registros cuyo Id sea igual al de los álbumes que estamos buscando.
             * 2.- Ordenamos el conjunto de registros por la fecha (de más antiguo a más actual).
             */

            if (release != null && release.Data.Count > 1)
                release.Data = release.Data.Where(item => item.Releasegroup.Id == releaseGroupId && item.Status == "Official" && item.Releasegroup.Primarytype == "Album")
                    .OrderBy(item => Utils.GetDateTime(item.Date)).ToList();

            return release;
        }
           
        private static MusicBrainz.Data.Release GetMusicBrainzData(string queryString)
        {
            int times = 0;
            int timeCount = 2;
            MusicBrainz.Data.Release release = null;

            do
            {
                //release = MusicBrainz.Search.Release(rgid: releaseGroupId, status: "official", primarytype: "album", limit: 100); //only values from 0 to 100 are valid; default: 25 
                release = MusicBrainz.Search.Release(query: queryString, limit: 100); //only values from 0 to 100 are valid; default: 25
                times++;

                /* 
                 * All users of the XML web service must ensure that each of their client applications never make more than ONE web service call per second. 
                 * Source: https://musicbrainz.org/doc/XML_Web_Service/Rate_Limiting 
                 */

                if (release == null) 
                    Thread.Sleep(1000);
            }
            while (release == null && times < timeCount);

            if (release == null)
                throw new Exception("MUSICBRAINZ: Petición no resuelta.");

            return release;
        }
          
        /*public static string EncodeInvalidChars(string str)
        {
            //UTF8Encoding utf8 = new UTF8Encoding();
            //return utf8.GetString(Encoding.GetEncoding(1252).GetBytes(str));
            UTF8Encoding utf8 = new UTF8Encoding();
            return utf8.GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(str));
        }*/

        public static string RemoveInvalidChars(string str)
        {
            /* Corregimos los errores producidos por una mala codificación de los caracteres */
            
            UTF8Encoding utf8 = new UTF8Encoding();
            str = utf8.GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(str));

            //str = Utils.EncodeInvalidChars(str);

            /* Quitamos los caracteres no válidos */

            //str = System.Text.RegularExpressions.Regex.Replace(str, @"[^\w\s\(\)-:]", String.Empty);//@"[<>¡!¿?/\\:;,.'-]+", "");

            /* Quitamos los espacios en blanco sobrantes */

            return System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ");
        }

        /*
         * Levenshtein distance. 
         * In computer science, "edit distance" is a way of quantifying how dissimilar two strings are to one another 
         * by counting the minimum number of operations required to transform one string into the other. 
         * One of the most common variants is called Levenshtein distance, named after the Soviet Russian computer 
         * scientist Vladimir Levenshtein. In this version, the allowed operations are the removal or insertion of a 
         * single character, or the substitution of one character for another.
         * Soruce: http://en.wikipedia.org/wiki/Levenshtein_distance
         */
        public static int LevenshteinDistance(string str1, string str2)
        {
            // degenerate cases
            if (str1 == str2) return 0;
            if (str1.Length == 0) return -1;
            if (str2.Length == 0) return -1;

            // create two work vectors of integer distances
            int[] v0 = new int[str2.Length + 1];
            int[] v1 = new int[str2.Length + 1];

            // initialize v0 (the previous row of distances)
            // this row is A[0][i]: edit distance for an empty s
            // the distance is just the number of characters to delete from t
            for (int i = 0; i < v0.Length; i++)
                v0[i] = i;

            for (int i = 0; i < str1.Length; i++)
            {
                // calculate v1 (current row distances) from the previous row v0

                // first element of v1 is A[i+1][0]
                //   edit distance is delete (i+1) chars from s to match empty t
                v1[0] = i + 1;

                // use formula to fill in the rest of the row
                for (int j = 0; j < str2.Length; j++)
                {
                    var cost = (str1[i] == str2[j]) ? 0 : 1;
                    v1[j + 1] = Math.Min(Math.Min(v1[j] + 1, v0[j + 1] + 1), v0[j] + cost);
                }

                // copy v1 (current row) to v0 (previous row) for next iteration
                for (int j = 0; j < v0.Length; j++)
                    v0[j] = v1[j];
            }

            return v1[str2.Length];
        }

        public static void SerializeJSON(string file, List<AcoustID.Web.LookupResult> results)
        {
            using (System.IO.StreamWriter txt = System.IO.File.CreateText(file))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializer.Serialize(txt, results);
            }
        }

        public static void SerializeJSON(string file, MusicBrainz.Data.Release release)
        {
            using (System.IO.StreamWriter txt = System.IO.File.CreateText(file))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializer.Serialize(txt, release);
            }
        }
    }
}
