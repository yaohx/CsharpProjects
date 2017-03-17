using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

public class StreamHelper
{
    public T DealFor<T>(Stream inStream) where T:class,new()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        T entity = new T();
        try
        {
           entity= xmlSerializer.Deserialize(inStream) as T;
        }
        catch (Exception ex)
        {
            
            throw;
        }
        return entity; 
    }
    public Stream DealWith<T>(T entity) where T : class,new()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        Stream outStream=new MemoryStream();
        try
        {
            xmlSerializer.Serialize(outStream, entity);
        }
        catch (Exception ex)
        {

            throw;
        }
        return outStream;
    }
}