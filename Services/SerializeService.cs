using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace ArthurCallouts.Services
{
    internal class SerializeService
    {
        public void SerializeToFile<T>(string filePath, T data)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, data);
            }
        }

        public T DeserializeFromFile<T>(string filePath)
        {
            if (!FileExists(filePath))
            {
                // Você pode retornar um valor padrão, lançar uma exceção personalizada ou logar um erro aqui
                return default(T);
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(fs);
            }
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
