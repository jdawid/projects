using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HelpdeskBusinessDataObjects
{
    public class ConfigBusinessData
    {

            ///<summary>
            ///Common Error Routine
            ///</summary>
            ///<param name="e">Exception thrown</param>
            ///<param name="obj">Class throwing exception</param>
            ///<param name="method">method throwing exception</param>
            public static void ErrorRoutine(Exception e, string obj, string method)
            {
                //debug to console to get around privilage issues with writing to log file 
                //during development 
                if (e.InnerException != null)
                {
                    Debug.WriteLine("Error in HelpDeskDataObjects, objects=" + obj +
                        ",method=" + method +
                        ", inner exception message=" +
                        e.InnerException.Message, EventLogEntryType.Error);
                    throw e.InnerException;
                }
                else
                {
                    Debug.WriteLine("Error in HelpdeskDataObjects, object=" + obj +
                        ",message=" + method + " , message=" +
                        e.Message, EventLogEntryType.Error);

                    throw e;
                }

            }


            ///<summary>
            ///Serializer
            ///</summary>
            ///<param name="inObject">Object to be serialized</param>
            ///<param name="bIsEntity">Is Object an Entity?</param>
            ///<returns>Serialized Object or Entity in byte array formar</returns>
            public static byte[] Serializer(Object inObject, bool bIsEntity = false)
            {
                byte[] ByteArrayObject;

                if (bIsEntity) //if entity use DataContractSerializer
                {
                    MemoryStream strm = new MemoryStream();
                    var serializer = new DataContractSerializer(inObject.GetType());
                    serializer.WriteObject(strm, inObject);
                    ByteArrayObject = strm.ToArray();
                }
                else
                {
                    BinaryFormatter frm = new BinaryFormatter();
                    MemoryStream strm = new MemoryStream();
                    frm.Serialize(strm, inObject);
                    ByteArrayObject = strm.ToArray();
                }
                return ByteArrayObject;
            }

            ///<summary>
            ///Deserializer
            ///</summary>
            ///<param name="ByteArrayIn">Serialized Object from BU Layer</param>
            ///<param name="entityType">Entity Type from BU layer</param>
            ///<returns>Reconstructed Object</returns>
            public static Object Deserializer(Byte[] ByteArrayIn, Type entityType)
            {
                MemoryStream stream = new MemoryStream(ByteArrayIn);
                DataContractSerializer ser = new DataContractSerializer(entityType);
                Object returnObject = ser.ReadObject(stream);
                return returnObject;
            }

            ///<summary>
            ///overload deserializer for non-entity classes
            ///</summary>
            ///<param name="ByteArrayIn"></param>
            ///<returns></returns>
            public static Object Deserializer(byte[] ByteArrayIn)
            {
                BinaryFormatter frm = new BinaryFormatter();
                MemoryStream strm = new MemoryStream(ByteArrayIn);
                Object returnObject = frm.Deserialize(strm);
                return returnObject;
            }



        }
    }

