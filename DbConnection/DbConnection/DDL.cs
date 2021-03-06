﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DbConnection
{
    class DDL
    {
        SqlCommand cmd;
        SqlDataAdapter adp;
        DataTable dt;
        Connect conn;

       public DDL()
        {
            try
            {
                conn = new Connect();
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        public void DDLCountry(ComboBox ddlcountry)
        {
            try
            {
                adp = new SqlDataAdapter("SELECT * FROM COUNTRY", conn.OpenConn());
                dt = new DataTable();
                adp.Fill(dt);
                ddlcountry.DataSource = dt;
                ddlcountry.DisplayMember = "COUNTRY_NAME";
                ddlcountry.ValueMember = "COUNTRY_ID";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DDLProvince(ComboBox ddlprovince, int COUNTRY_ID)
        {
            try
            {
                adp = new SqlDataAdapter("SELECT * FROM PROVINCE WHERE C_ID = " + COUNTRY_ID + "", conn.OpenConn());
                dt = new DataTable();
                adp.Fill(dt);
                ddlprovince.DataSource = dt;
                ddlprovince.DisplayMember = "PROVINCE_NAME";
                ddlprovince.ValueMember = "PROVINCE_ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DDLCity(ComboBox ddlcity, int PROVINCE_ID)
        {
            try
            {
                adp = new SqlDataAdapter("SELECT * FROM CITY WHERE PRO_ID = " + PROVINCE_ID + "", conn.OpenConn());
                dt = new DataTable();
                adp.Fill(dt);
                ddlcity.DataSource = dt;
                ddlcity.DisplayMember = "CITY_NAME";
                ddlcity.ValueMember = "CITY_ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string Insert(string FIRST_NAME, string LAST_NAME, string FATHER_FIRST_NAME, string FATHER_LAST_NAME, string SEX, string DATE_OF_BIRTH, string EMAIL, string MOBILE, string MAILING_ADDRESS, int CITY_ID, int PROVINCE_ID, int COUNTRY_ID)
        {
            string msg = "";
            try
            {
                if(FIRST_NAME != null && LAST_NAME != null && FATHER_FIRST_NAME != null && FATHER_LAST_NAME != null && SEX != null && DATE_OF_BIRTH != null && EMAIL != null && MOBILE != null && MAILING_ADDRESS != null && CITY_ID != 0 && PROVINCE_ID != 0 && COUNTRY_ID != 0)
                {
                    cmd = new SqlCommand("INSERT INTO FORM VALUES('" + FIRST_NAME + "', '" + LAST_NAME + "', '" + FATHER_FIRST_NAME + "', '" + FATHER_LAST_NAME + "','" + SEX + "', '" + DATE_OF_BIRTH + "','" + EMAIL + "', '" + MOBILE + "','" + MAILING_ADDRESS + "', '" + CITY_ID + "','" + PROVINCE_ID + "', '" + COUNTRY_ID + "')", conn.OpenConn());

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        msg = "Record Entered Successfully.";
                    }

                    else
                    {
                        msg = "Please enter the values.";
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return msg;
        }

        public DataTable DataView()
        {
            adp = new SqlDataAdapter("SELECT FORM.FORM_ID, FORM.FIRST_NAME, FORM.LAST_NAME, FORM.FATHER_FIRST_NAME, FORM.FATHER_LAST_NAME, FORM.SEX, FORM.DATE_OF_BIRTH, FORM.EMAIL, FORM.MOBILE, FORM.MAILING_ADDRESS, CITY.CITY_NAME, PROVINCE.PROVINCE_NAME, COUNTRY.COUNTRY_NAME FROM FORM INNER JOIN CITY ON FORM.CITY_ID_2 = CITY.CITY_ID INNER JOIN PROVINCE ON FORM.PROVINCE_ID_2 = PROVINCE.PROVINCE_ID INNER JOIN COUNTRY ON FORM.COUNTRY_ID_2 = COUNTRY.COUNTRY_ID", conn.OpenConn());
            dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }

        public string Update(int R_ID, string FIRST_NAME, string LAST_NAME, string FATHER_FIRST_NAME, string FATHER_LAST_NAME, string SEX, string DATE_OF_BIRTH, string EMAIL, string MOBILE, string MAILING_ADDRESS, int CITY_ID, int PROVINCE_ID, int COUNTRY_ID)
        {
            string msg = "";
            try
            {
                cmd = new SqlCommand("UPDATE FORM SET FIRST_NAME = '" + FIRST_NAME + "', LAST_NAME = '" + LAST_NAME + "', FATHER_FIRST_NAME = '" + FATHER_FIRST_NAME + "', FATHER_LAST_NAME = '" + FATHER_LAST_NAME + "', SEX = '" + SEX + "', DATE_OF_BIRTH = '" + DATE_OF_BIRTH + "', EMAIL = '" + EMAIL + "', MOBILE = '" + MOBILE + "', MAILING_ADDRESS = '" + MAILING_ADDRESS + "', CITY_ID_2 = '" + CITY_ID + "', PROVINCE_ID_2 = '" + PROVINCE_ID + "', COUNTRY_ID_2 = '" + COUNTRY_ID + "' WHERE FORM_ID = '"+R_ID+"'", conn.OpenConn());

                if(FIRST_NAME != null && LAST_NAME != null && FATHER_FIRST_NAME != null && FATHER_LAST_NAME != null && SEX != null && DATE_OF_BIRTH != null && EMAIL != null && MOBILE != null && MAILING_ADDRESS != null && CITY_ID != 0 && PROVINCE_ID != 0 && COUNTRY_ID != 0)
                {
                    cmd.ExecuteNonQuery();
                    msg = "Data Updated";
                }
                else
                {
                    msg = "Updation Unsuccessful.";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return msg;
        }

        public void Delete(int del)
        {
            cmd = new SqlCommand("DELETE FROM FORM WHERE FORM_ID = '" + del + "'", conn.OpenConn());
            cmd.ExecuteNonQuery();
        }
    }
}
