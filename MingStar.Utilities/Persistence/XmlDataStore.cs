using System.IO;
using System.Xml.Serialization;

namespace MingStar.Utilities.Persistence
{
    public class XmlDataStore<TData>
    {
        private static XmlSerializer m_xs = new XmlSerializer(typeof (TData));

        public static void Serialize(string filename, TData data)
        {
            Stream stream = null;
            try
            {
                stream = File.Open(filename, FileMode.Create);
                m_xs.Serialize(stream, data);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public static TData Deserialize(string filename)
        {
            Stream stream = null;
            try
            {
                stream = File.Open(filename, FileMode.Open);
                return (TData) m_xs.Deserialize(stream);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }
    }
}