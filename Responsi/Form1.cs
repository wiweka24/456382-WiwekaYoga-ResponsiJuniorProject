using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Responsi
{
    public partial class Form1 : Form
    {
        NpgsqlConnection conn;
        NpgsqlCommand cmd;
        NpgsqlDataReader dr;
        DataTable karyawan;
        int currentidDep = 1; //sementara
        char currentidKar = '1'; //sementara

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeConnection();
            getKaryawan();
        }

        public void InitializeConnection()
        {
            var connStr = String.Format(
                "Server=localhost, " +
                "Database=responsiweka, " +
                "User Id=postgres, " +
                "Password=informatika, " +
                "Port=2022");

            conn = new NpgsqlConnection(connStr);
            cmd = new NpgsqlCommand();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            addKaryawan();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editKaryawan();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteKaryawan();
        }

        public void getKaryawan()
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = String.Format(
                "SELECT * FROM karyawan");
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                karyawan = new DataTable();
                karyawan.Load(dr);
            };

            conn.Dispose();
            conn.Close();
        }

        public void addKaryawan()
        {
            string nama = tbNama.ToString();
            int id_dep = currentidDep;

            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = String.Format(
                "INSERT INTO karyawan (nama, id_dep)" +
                "VALUES ({0}, {1}",
                nama, id_dep);
            cmd.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();
        }

        public void editKaryawan()
        {
            string nama = tbNama.ToString();
            int id_dep = currentidDep;
            int id_karyawan = currentidKar;

            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = String.Format(
                "UPDATE karyawan" +
                "SET nama = {0}, id_dep = {1}" +
                "WHERE id_karyawan = {3}",
                nama, id_dep, id_karyawan);
            cmd.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();
        }

        public void deleteKaryawan()
        {
            int id_karyawan = currentidKar;

            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = String.Format(
                "DELETE FROM karyawan" +
                "WHERE id_karyawan = {0}",
                currentidKar);
            cmd.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();
        }


    }
}
