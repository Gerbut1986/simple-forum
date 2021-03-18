namespace My_Forum_Web.Models
{
    using System;
    using System.IO;
    using System.Data;
    using System.Drawing;
    using System.Data.Common;
    using System.Configuration;
    using System.Data.SqlClient;

    public class ImageUpload
    {
        static SqlConnection conn = null;
        static SqlCommand cmd = null;

        public static string InsertImg(string img_name) // @"Alien 1.bmp"
        {
            string res = string.Empty;
            using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand("INSERT INTO MyUsers (image) VALUES (@img)", conn))
                {
                    SqlParameter sqlParameter = new SqlParameter("@img", SqlDbType.VarBinary);
                    string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\" + img_name;
                    //Путь к файлу
                    Image image = Image.FromFile(fileName);
                    //Изображение из файла.
                    MemoryStream memoryStream = new MemoryStream();                                  //Поток в который запишем изображение
                    image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    sqlParameter.Value = memoryStream.ToArray();
                    cmd.Parameters.Add(sqlParameter);
                    memoryStream.Dispose();
                    int res_msg = cmd.ExecuteNonQuery();
                    if (res_msg == 1) res = "Image has writed successfuly!";
                    else res = "Something went wrong";
                }
            }
            return res;
        }

        public static string GetImg(string img_name)
        {
            string res = string.Empty;
            using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand("SELECT image FROM tab WHERE id = 1", conn))
                {
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        foreach (DbDataRecord record in sqlDataReader)
                            memoryStream.Write((byte[])record["image"], 0, ((byte[])record["image"]).Length);
                        Image image = Image.FromStream(memoryStream);
                        image.Save(@"C:\1.BMP");
                        memoryStream.Dispose();
                        image.Dispose();
                        res = @"Image was Save on path - C:\1.BMP";
                    }
                    else
                        res= "Пустая выборка";
                }
            }
            return res;
        }
    }
}