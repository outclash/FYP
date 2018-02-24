using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class DBManager : MonoBehaviour {

	private string dbPath;
	// Use this for initialization
	void Start () {
		dbPath = "URI=file:" + Application.dataPath + "/gameDB.sqlite";
		createDBTable ();
		newAccount("user1","password","sas");
	}

	//runs once to create account and profile tables
	public void createDBTable(){ 
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					string query = "CREATE TABLE IF NOT EXISTS `Account` (`accID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `Username` VARCHAR(20) NOT NULL UNIQUE, `Password` VARCHAR(20) NOT NULL );";
					dbCmd.CommandText = query;
					int res = dbCmd.ExecuteNonQuery ();
					Debug.Log ("table result: " + res); //0 = success

					query = "CREATE TABLE IF NOT EXISTS `Profile` (`userID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 'CharName' VARCHAR(20) NOT NULL UNIQUE, `Score` INTEGER, `PU1` INTEGER, `PU2` INTEGER, 'Username' VARCHAR(20) NOT NULL, FOREIGN KEY(Username) REFERENCES Account(Username));";
					dbCmd.CommandText = query;
					res = dbCmd.ExecuteNonQuery ();

					Debug.Log ("table result prof: " + res); //0 = success using create
				}
			 
			} catch (Exception e) {
				Debug.Log ("Error creating tables");
			} finally{
				conn.Close ();
			}
		}
	}

	//adds new account and profile
	public void newAccount (string uname, string pw, string cname)
	{
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					string query = "INSERT INTO Account (Username, Password) VALUES (@usname, @passw);";
					dbCmd.CommandText = query;

					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = uname });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "passw", Value = pw });
					int res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					Debug.Log ("table result insert: " + res); //1 = success when using insert

					if(res == 1 ){
						query = "INSERT INTO Profile (CharName, Score, PU1, PU2, Username) VALUES (@chname, @score, @pu1, @pu2, @usname);";
						dbCmd.CommandText = query;

						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "chname", Value = cname });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "score", Value = 0 });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu1", Value = 0 });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu2", Value = 0 });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = uname });
						res = dbCmd.ExecuteNonQuery ();
						//object sd = dbCmd.ExecuteScalar ();
						Debug.Log ("table result insert: " + res); //1 = success when using insert
					}
				}
			} catch (Exception e) {
				Debug.Log ("Error creating prrofile");
				Debug.Log (e);
			} finally {
				conn.Close ();
			}
		}
	}
}


//
//				using(IDataReader reader = dbCmd.ExecuteReader()){
//
//					while (reader.Read ()) {
//						Debug.Log (reader.GetString (0));
//					}
//
//					conn.Close ();
//					reader.Close ();
//				}