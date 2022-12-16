// Класс описывающий карту
using ImageMagick;
using System;
using System.IO;
using System.Text;

class Map{
    int[][][] map3d;

    public Map(String filename, int minHight, int maxHight, int maxSky){
        using (MagickImage image = new MagickImage(filename))
        {
            // Инициализация трёхмерного пространства
            map3d = new int[image.Width][][];
            for (int i = 0; i < image.Width; i++)
                map3d[i] = new int[image.Height][];
            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                    map3d[i][j] = new int[maxHight - maxHight + maxSky];
            
            // Обнуляем массив (0 - ничего нет)
            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                    for (int z = 0; z < map3d[i][j].Length; z++)
                        map3d[i][j][z] = 0;

            // Обрабатываем изображение
            IPixelCollection collection = image.GetPixels();

            for (int i = 0; i < image.Height; i++){
                for (int j = 0; j < image.Width; j++){
                    // TODO: Кривая нормализация
                    float time = (float)255 / (float)(maxHight - minHight + maxSky);
                    float myBase = (float)collection.GetPixel(j, i).GetChannel(0); // myBase [0 .. 255]
                    
                    int z = (int)(myBase / time);

                    if (z >= map3d[i][j].Length)
                        z = map3d[i][j].Length;
                    if (z < 0)
                        z = 0;

                    map3d[j][i][z] = 1;
                    for (int k = 0; k < z; k++)
                        map3d[j][i][z] = 1;
                }
            }
        }
        
    }

    public void exportToFile(String filename){
        String str = "# Craeted me to me" + Environment.NewLine;
        
        for (int i = 0; i < map3d.Length; i++)
            for (int j = 0; j < map3d[i].Length; j++)
                for (int z = 0; z < map3d[i][j].Length; z++)
                    if (map3d[i][j][z] == 1)
                        str += i + " " + j + " " + z + Environment.NewLine;
        try
        {
            // Create the file, or overwrite if the file exists.
            using (FileStream fs = File.Create(filename))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(str);
                fs.Write(info, 0, info.Length);
            }
        } catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}