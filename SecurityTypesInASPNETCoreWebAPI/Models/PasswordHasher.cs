namespace SecurityTypesInASPNETCoreWebAPI.Models
{
    // This class is used to hash the password and verify the password
    public static class PasswordHasher
    {

        // This method is used to create the password hash and password salt
        // password : This is the password which we want to hash and it is plain text
        // passwordHash : This is the output parameter which will store the password hash
        // passwordSalt : This is the output parameter which will store the password salt
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // It will instantite HMACSHA512 to generate a cryptographically hash and unique salt value.
            // Here Using statement is used to dispose the object after the use
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //The below key property of HMACSHA512 it will provide a random generated salt.
                passwordSalt = hmac.Key; //Assign the generated salt to 

                //Convert the plaintext password to byte array
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

                //Compute the hash of the password using the ComputeHash method of HMACSHA512
                passwordHash = hmac.ComputeHash(passwordBytes);

            }
        }

        // This method is used to verify the password hash
        // password : This is the password which we want to verify
        // passwordHash : The stored password hash to compare against to the user given value
        // passwordSalt : The stored salt used during the original password hash creation
        // Return : It will return true if the password is correct otherwise false
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            // It will instantite HMACSHA512 to generate a cryptographically hash and unique salt value.
            // Here Using statement is used to dispose the object after the use
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                //Convert the plaintext password to byte array
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

                //Compute the hash of the password using the ComputeHash method of HMACSHA512
                byte[] computedHash = hmac.ComputeHash(passwordBytes);

                //Compare the computed hash with the stored hash
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }


    }
}
