﻿using Advocacy_Software.Advocacy.Software.Entities;

namespace Advocacy_Software.Advocacy.Software.Shared.SqlCommands
{
    public static class LawyerSqlCommands
    {
        public static string Create(Lawyer lawyer) => $@"INSERT INTO lawyers 
        (Name, Nationality, CivilStatus, Profession, Phone, Email, 
        RegisterDate, LastUpdate, Id, IdAddress, OabNumber, OabUf, IdSignature, AppPassword) 
        VALUES ('{lawyer.Name}', '{lawyer.Nationality}', '{lawyer.CivilStatus}', '{lawyer.Profession}', 
        '{lawyer.Phone}', '{lawyer.Email}', 
        '{lawyer.RegisterDate}', '{lawyer.LastUpdate}', '{lawyer.Id}', '{lawyer.IdAddress}', '{lawyer.OabNumber}', '{lawyer.UfOab}', 
        {lawyer.IdSignature}, '{lawyer.AppPassword}');";

        public static string Read(int IdSignature) => $"SELECT * FROM lawyers WHERE IdSignature = {IdSignature}";

        public static string Update(Lawyer lawyer) => $@"UPDATE lawyers 
        SET Name = '{lawyer.Name}', Nationality = '{lawyer.Nationality}', CivilStatus = '{lawyer.CivilStatus}', 
        Profession = '{lawyer.Profession}',  
        Phone = '{lawyer.Phone}', Email = '{lawyer.Email}', RegisterDate = '{lawyer.RegisterDate}', LastUpdate = '{lawyer.LastUpdate}', 
        Id = '{lawyer.Id}', IdAddress = {lawyer.IdAddress}, OabNumber = '{lawyer.OabNumber}', OabUf = '{lawyer.UfOab}',
        IdSignature = {lawyer.IdSignature}, AppPassword = '{lawyer.AppPassword}' 
        WHERE Id = '{lawyer.Id}' AND IdSignature = {lawyer.IdSignature}";

        public static string Delete(Lawyer lawyer) => $"DELETE FROM lawyers WHERE IdAddress = {lawyer.IdAddress} AND IdSignature = {lawyer.IdSignature} AND Id = '{lawyer.Id}'";

        public static string Delete(int IdSignature) => $"DELETE FROM lawyers WHERE IdSignature = {IdSignature}";

        public static string Read(Lawyer lawyer) => $"SELECT * FROM lawyers WHERE Name = '{lawyer.Name}' AND IdSignature = {lawyer.IdSignature};";
    }
}
