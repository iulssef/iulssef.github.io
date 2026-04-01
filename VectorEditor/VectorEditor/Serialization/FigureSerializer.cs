using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using VectorEditor.Figures;

namespace VectorEditor.Serialization
{
    public static class FigureSerializer
    {
        public static void SaveToStream(Stream stream, List<Figure> figures)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, figures);
            stream.Position = 0;
        }

        public static List<Figure> LoadFromStream(Stream stream)
        {
            try
            {
                stream.Position = 0;
                var formatter = new BinaryFormatter();
                return (List<Figure>)formatter.Deserialize(stream);
            }
            catch
            {
                return new List<Figure>();
            }
        }

        public static bool SaveToFile(string fileName, List<Figure> figures)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create))
                    SaveToStream(fs, figures);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
                return false;
            }
        }

        public static List<Figure> LoadFromFile(string fileName)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open))
                    return LoadFromStream(fs);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
                return new List<Figure>();
            }
        }
    }
}