using System;

namespace Core.Utils.Constants
{
    public class Messages
    {
        private static Messages instance;  

        public static Messages Instance
        {
            get
            {
                return instance ??= new Messages();  
            }
        }
     
        private Messages()
        {
 
        }
 


    }
}
