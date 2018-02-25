using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class DBManager : MonoBehaviour
{
	private string dbPath;
	public String logName;
	public int charCount;
	public List<ProfileList> profList = new List<ProfileList> ();

	// Use this for initialization
	void Start ()
	{
		dbPath = "URI=file:" + Application.dataPath + "/gameDB.sqlite";
		//loginAcc ("user1","password");
		//createDBTable (); 
		//newAccount ("user1", "password", "sas");
		//newCharProfile("user1","bas");
		//deleteProfile("sas");
		//readProfile ("user1");
		//deleteAccount ("user1");
		//updateProfile("bas","14124.33",213,4,66);
		//readProfile ("user1");
	}

	//runs once to create account and profile tables
	public void createDBTable ()
	{ 
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					string query = "CREATE TABLE IF NOT EXISTS `Account` (`accID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `Username` VARCHAR(20) NOT NULL UNIQUE, `Password` VARCHAR(20) NOT NULL, 'CharCount' INTEGER );";
					dbCmd.CommandText = query;
					int res = dbCmd.ExecuteNonQuery ();
					Debug.Log ("table result: " + res); //0 = success

					query = "CREATE TABLE IF NOT EXISTS `Profile` (`profID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 'CharName' VARCHAR(20) NOT NULL UNIQUE, 'Score' TEXT, 'Coins' INTEGER, `PU1` INTEGER, `PU2` INTEGER, 'Username' VARCHAR(20) NOT NULL, FOREIGN KEY(Username) REFERENCES Account(Username));";
					dbCmd.CommandText = query;
					res = dbCmd.ExecuteNonQuery ();

					Debug.Log ("table result prof: " + res); //0 = success using create
				}
			 
			} catch (Exception e) {
				Debug.Log ("Error creating tables");
			} finally {
				conn.Close ();
			}
		}
	}

	//check user  login
	public void loginAcc (string username, string pw)
	{
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					string query = "SELECT Password, CharCount FROM Account WHERE Username = @usname";
					dbCmd.CommandText = query;
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = username });

					int res = dbCmd.ExecuteNonQuery ();
					Debug.Log ("login valid: " + res); //0 = success when using insert

					if (res == 0) {
						using (IDataReader reader = dbCmd.ExecuteReader ()) {
							while (reader.Read ()) {
								//insert decryption here
								if (reader.GetString (0) == pw) {
									logName = username;
									charCount = reader.GetInt32 (1);
									Debug.Log (logName + charCount);
								}
							}
							reader.Close ();
						}
	
					}
				}

			} catch (Exception e) {
				Debug.Log ("Error login");
			} finally {
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
					string query = "INSERT INTO Account (Username, Password, CharCount) VALUES (@usname, @passw, @cc);";
					dbCmd.CommandText = query;

					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = uname });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "passw", Value = pw });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "cc", Value = 1 });
					int res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					Debug.Log ("table result insert acc: " + res); //1 = success when using insert

					if (res == 1) {
						query = "INSERT INTO Profile (CharName, Score, Coins, PU1, PU2, Username) VALUES (@chname, @score, @coin, @pu1, @pu2, @usname);";
						dbCmd.CommandText = query;

						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "chname", Value = cname });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "score", Value = "0" });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "coin", Value = 0 });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu1", Value = 0 });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu2", Value = 0 });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = uname });
						res = dbCmd.ExecuteNonQuery ();
						//object sd = dbCmd.ExecuteScalar ();
						Debug.Log ("table result insert prof: " + res); //1 = success when using insert
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

	public void newCharProfile (string uname, string cname)
	{
		//if(username.charcount is < 4) charcount++, update account charcount +1
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					
					string query = "INSERT INTO Profile (CharName, Score, Coins, PU1, PU2, Username) VALUES (@chname, @score, @coin, @pu1, @pu2, @usname);";
					dbCmd.CommandText = query;

					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "chname", Value = cname });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "score", Value = "0" });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "coin", Value = 0 });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu1", Value = 0 });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu2", Value = 0 });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = uname });
					int res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					Debug.Log ("succes new cahr: " + res); //1 = success when using insert

					//update account character count
					query = "UPDATE Account SET CharCount = @cc WHERE Username = @usname;";
					dbCmd.CommandText = query;

					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "cc", Value = 2 }); //value = uname.charcount++
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = uname });

					res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					Debug.Log ("update account charcount: " + res); //1 = success when using insert

				}
			} catch (Exception e) {
				Debug.Log ("Error creating new char prrofile");
				Debug.Log (e);
			} finally {
				conn.Close ();
			}
		}
	}

	public void readProfile (string uname)
	{ 
		profList.Clear ();
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					string query = "SELECT * FROM Profile WHERE Username = @usname";
					dbCmd.CommandText = query;
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = uname });

					using (IDataReader reader = dbCmd.ExecuteReader ()) {
						while (reader.Read ()) {

							profList.Add(new ProfileList(reader.GetString (1), reader.GetString (2), reader.GetInt32 (3), reader.GetInt32 (4), reader.GetInt32 (5)));
							//Debug.Log (reader.GetString (1) + ", " + reader.GetString (2) + ", " + reader.GetInt32 (3) + ", " + reader.GetInt32 (4) + ", " + reader.GetInt32 (5) + ", " + reader.GetString (6));
						}
						reader.Close ();
					}

				}

			} catch (Exception e) {
				Debug.Log ("Error getting all profile");
			} finally {
				conn.Close ();
			}
		}
	}

	public void updateProfile (string cname, string scr, int coin, int pu1, int pu2)
	{ 
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					string query = "UPDATE Profile SET Score = @score, Coins = @coin, PU1 = @pu1, PU2 = @pu2 WHERE CharName = @chname";
					dbCmd.CommandText = query;

					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "chname", Value = cname });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "score", Value = scr });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "coin", Value = coin });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu1", Value = pu1 });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu2", Value = pu2 });

					int res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					Debug.Log ("update prof only: " + res); //1 = success when using insert
				}

			} catch (Exception e) {
				Debug.Log ("Error upd 1 prof ");
			} finally {
				conn.Close ();
			}
		}
	}

	public void deleteAccount (string uname)
	{ 
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					//delete child all profiles first
					string query = "DELETE FROM Profile WHERE Username = @usname";
					dbCmd.CommandText = query;
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = uname });

					int res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					Debug.Log ("del prof : " + res); //1 = success when using insert

					//delete account
					query = "DELETE FROM Account WHERE Username = @usname";
					dbCmd.CommandText = query;
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = uname });
					res = dbCmd.ExecuteNonQuery ();
					Debug.Log ("del acc : " + res); //1 = success when using insert
				}

			} catch (Exception e) {
				Debug.Log ("Error delete all ");
			} finally {
				conn.Close ();
			}
		}
	}

	public void deleteProfile (string cname)
	{ 
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					//delete child all profiles first
					string query = "DELETE FROM Profile WHERE CharName = @chname";
					dbCmd.CommandText = query;
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "chname", Value = cname });

					int res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					Debug.Log ("del prof only: " + res); //1 = success when using insert
				}

			} catch (Exception e) {
				Debug.Log ("Error delete 1 prof ");
			} finally {
				conn.Close ();
			}
		}
	}
}


