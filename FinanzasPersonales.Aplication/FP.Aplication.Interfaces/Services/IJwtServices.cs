﻿using FinanzasPersonales.Domain;

namespace FinanzasPersonales.Aplication
{
    public interface IJwtServices
    {
        public string GenerateTokenLogin(User user);
        public string GenerateTokenRetrievePassword(User user);

        //public string RefreshToken(User User);



    }
}
