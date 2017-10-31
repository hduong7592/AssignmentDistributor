using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for Security
/// </summary>
public class Security
{
    public Security()
    {
        //
        // TODO: Add constructor logic here
        //
    }    

    public static string EncodePassword(string value)
    {

        string encodedpasswordstring = "";
        byte[] encodedpassword;
        byte[] sha1hash;

        System.Security.Cryptography.SHA1Managed hash = new System.Security.Cryptography.SHA1Managed();

        // get the byte representation of the password
        encodedpassword = Encoding.ASCII.GetBytes(value);

        // Compute the SHA1 hash of the password.
        sha1hash = hash.ComputeHash(encodedpassword);

        // String builder to create the final Hex encoded hash string
        StringBuilder hashedkey = new StringBuilder(sha1hash.Length);


        // Convert to Hex encoded string
        foreach (byte b in sha1hash)
        {
            // This generates the hex encoded string in lower case. Use "X2" to get hash string in upper-case.
            hashedkey.Append(b.ToString("x2"));
        }

        encodedpasswordstring = hashedkey.ToString();  // Final Hex encoded SHA1 hash string

        return encodedpasswordstring;
    }
}