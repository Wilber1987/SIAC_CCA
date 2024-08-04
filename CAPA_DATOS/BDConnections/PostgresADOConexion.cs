﻿using System;
using System.Collections.Generic;
using System.Text;
using CAPA_DATOS.BDCore;
using CAPA_DATOS.BDCore.Abstracts;
using CAPA_DATOS.PostgresImplementations;

namespace CAPA_DATOS;

public class PostgresADOConnection
{
    public static WDataMapper? SQLM;
    static public bool IniciarConexion(string SGBD_USER, string SWGBD_PASSWORD, string SQLServer, string BDNAME, int PORT)
    {
        try
        {
            return createConexion(SQLServer, SGBD_USER, SWGBD_PASSWORD, BDNAME,  PORT);
        }
        catch (Exception)
        {
            SQLM = null;
            return false;
            throw;
        }
    }   
    private static bool createConexion(string PostgreSQL, string SGBD_USER, string SWGBD_PASSWORD, string BDNAME, int Port = 5432)
    {
        string userSQLConexion = $"Host={PostgreSQL};Port={Port};Username={SGBD_USER};Password={SWGBD_PASSWORD};Database={BDNAME};";
        SQLM = new WDataMapper(new PostgreGDatos(userSQLConexion), new PostgreQueryBuilder());
        if (SQLM.GDatos.TestConnection()) {
            Connections.Default = SQLM;
            return true;
        } 
        else
        {
            SQLM = null;
            return false;
        }
    }
}


