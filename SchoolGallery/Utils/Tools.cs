using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolGallery.Utils
{
    public class Tools
    {
        public static List<SelectListItem> CreateTree(List<CategoryModel> items, int parentID, int depth)
        {
            var list = new List<SelectListItem>();
            var temItems = items.Where(z => z.ParentID == parentID).ToList();
            string tem = new string('┴', depth);

            string tem1 = depth == 0 ? tem1 = "├" : tem1 = "├";
            tem =  tem1 + tem;


            if (temItems.Count > 0)
            {
                for (int i = 0; i < temItems.Count; i++)
                {
                    list.Add(new SelectListItem() { Text = tem +' '+ temItems[i].Title, Value = temItems[i].ID.ToString() });
                    list.AddRange(CreateTree(items, temItems[i].ID, depth + 1));
                }
            }
            return list;

        }
        static string GetMd5Hash( string input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public static string DoubleMD5(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(GetMd5Hash(input));
            }

        }
        static bool VerifyDoubleMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
