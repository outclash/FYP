﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

/*
 * Class that manage the database CRUD
*/
public class DBManager : MonoBehaviour
{
	private static DBManager DB; 
	private string dbPath;
	public string logName;
	public int charCount;
	public string charname;
	public float currHighScore;
	public int currCoins;
	public int pu1Count;
	public int pu2Count;
	public List<ProfileList> profList = new List<ProfileList> ();

	public int coinGain; //store coin gain per game
	public float currScore; //store score per game

	void Awake(){
		//Set this Game manager to persist all the time
		if (DB == null) {
			DontDestroyOnLoad (gameObject);
			DB = this;
		} else if (DB != this) {
			Destroy (gameObject);
		}
		//connects the database.. if no database on the path then create new database
		dbPath = "URI=file:" + Application.dataPath + "/gameDB.sqlite";
	}
	void Start ()
	{
		logName = null;
		charCount = 0;
		charname = null;
		createDBTable (); 
	}

	//runs once to create account and profile tables
	public void createDBTable ()
	{ 
		//connection the SQL
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				//open the connection
				conn.Open ();
				//create a command executer
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					//make a SQL query
					string query = "CREATE TABLE IF NOT EXISTS `Account` (`accID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `Username` VARCHAR(20) NOT NULL UNIQUE, `Password` VARCHAR(20) NOT NULL, 'CharCount' INTEGER );";
					//add query and execute query 
					dbCmd.CommandText = query;
					int res = dbCmd.ExecuteNonQuery ();
					//Debug.Log ("table result: " + res); //0 = success

					query = "CREATE TABLE IF NOT EXISTS `Profile` (`profID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 'CharName' VARCHAR(20) NOT NULL UNIQUE, 'Score' TEXT, 'Coins' INTEGER, `PU1` INTEGER, `PU2` INTEGER, 'Username' VARCHAR(20) NOT NULL, FOREIGN KEY(Username) REFERENCES Account(Username));";
					dbCmd.CommandText = query;
					res = dbCmd.ExecuteNonQuery ();
				}
			 
			} catch (Exception e) {
				//can add debug for checking error e
				//Debug.Log ("Error creating tables");
			} finally {
			//close connection used
				conn.Close ();
			}
		}
	}

	//check user  login
	public Boolean loginAcc (string username, string pw)
	{
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					string query = "SELECT Password, CharCount FROM Account WHERE Username = @usname";
					dbCmd.CommandText = query;
					//parameterised query to avoid sql injections
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = username });
					int res = dbCmd.ExecuteNonQuery ();
					if (res == 0) {
						using (IDataReader reader = dbCmd.ExecuteReader ()) {
							while (reader.Read ()) {
								//insert decryption here
								String decpw = SecurityEncDec.SecurityMeasure.DecryptString(reader.GetString (0),username);
								if (decpw == pw) {
									logName = username;
									charCount = reader.GetInt32 (1);
									reader.Close ();
									return true;
								} else { 
									reader.Close ();
									return false;
								}
							}
							return false; 
						}
					} else {
						return false;
					}
				}

			} catch (Exception e) {
				//Debug.Log ("Error login");
				return false;
			} finally {
				conn.Close ();
			}
		}
	}
	//adds new account and profile
	public Boolean newAccount (string uname, string pw, string cname)
	{
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					string query = "INSERT INTO Account (Username, Password, CharCount) VALUES (@usname, @passw, @cc);";
					dbCmd.CommandText = query;
					string encpw = SecurityEncDec.SecurityMeasure.EncryptString(pw,uname);
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = uname });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "passw", Value = encpw });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "cc", Value = 1 });
					int res = dbCmd.ExecuteNonQuery ();
					//Debug.Log ("table result insert acc: " + res); //1 = success when using insert

					if (res == 1) {
						query = "INSERT INTO Profile (CharName, Score, Coins, PU1, PU2, Username) VALUES (@chname, @score, @coin, @pu1, @pu2, @usname);";
						dbCmd.CommandText = query;

						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "chname", Value = cname });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "score", Value = "0" });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "coin", Value = 10 });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu1", Value = 1 });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu2", Value = 1 });
						dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = uname });
						res = dbCmd.ExecuteNonQuery ();
						//object sd = dbCmd.ExecuteScalar ();
						//Debug.Log ("table result insert prof: " + res); //1 = success when using insert
						return true;
					} else {
						return false;
					}
				}
			} catch (Exception e) {
				//Debug.Log ("Error creating prrofile");
				//Debug.Log (e);
				return false;

			} finally {
				conn.Close ();
			}
		}
	}

	public Boolean newCharProfile (string cname)
	{
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					
					string query = "INSERT INTO Profile (CharName, Score, Coins, PU1, PU2, Username) VALUES (@chname, @score, @coin, @pu1, @pu2, @usname);";
					dbCmd.CommandText = query;

					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "chname", Value = cname });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "score", Value = "0" });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "coin", Value = 10 });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu1", Value = 1 });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu2", Value = 1 });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = logName });
					int res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					//Debug.Log ("succes new cahr: " + res); //1 = success 

					//update account character count
					query = "UPDATE Account SET CharCount = @cc WHERE Username = @usname;";
					dbCmd.CommandText = query;
					charCount++;
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "cc", Value = charCount });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = logName });

					res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					//Debug.Log ("update account charcount: " + res); 
					//Debug.Log ("charcount: " + charCount);//1 = success when using insert
					charname = null;
					readProfile ();
					return true;
				}
			} catch (Exception e) {
				//Debug.Log ("Error creating new char prrofile");
				////Debug.Log (e);
				return false;
			} finally {
				conn.Close ();
			}
		}
	}

	public void readProfile ()
	{ 
		profList.Clear ();
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					string query = "SELECT * FROM Profile WHERE Username = @usname";
					dbCmd.CommandText = query;
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = logName });
					//read data using IDataReader goes through every data
					using (IDataReader reader = dbCmd.ExecuteReader ()) {
						while (reader.Read ()) {
							profList.Add (new ProfileList (reader.GetString (1), reader.GetString (2), reader.GetInt32 (3), reader.GetInt32 (4), reader.GetInt32 (5)));
						}
						reader.Close ();
					}

				}

			} catch (Exception e) {
				//Debug.Log ("Error getting all profile");
			} finally {
				conn.Close ();
			}
		}
	}

	public void updateProfile ()
	{ 
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					string query = "UPDATE Profile SET Score = @score, Coins = @coin, PU1 = @pu1, PU2 = @pu2 WHERE CharName = @chname";
					dbCmd.CommandText = query;

					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "chname", Value = charname });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "score", Value = currHighScore.ToString ("F") });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "coin", Value = currCoins });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu1", Value = pu1Count });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "pu2", Value = pu2Count });

					int res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					readProfile ();
					//Debug.Log ("update prof only: " + res); //1 = success when using insert
				}

			} catch (Exception e) {
				//Debug.Log ("Error upd 1 prof ");
			} finally {
				conn.Close ();
			}
		}
	}

	public void afterGameUpdate(){
		if (currScore > currHighScore) {
			currHighScore = currScore;
		}
		currCoins += coinGain;
		updateProfile ();
	}

	public void deleteAccount ()
	{ 
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					//delete child all profiles first
					string query = "DELETE FROM Profile WHERE Username = @usname";
					dbCmd.CommandText = query;
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = logName });

					int res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					//Debug.Log ("del prof : " + res); //1 = success when using insert

					//delete account
					query = "DELETE FROM Account WHERE Username = @usname";
					dbCmd.CommandText = query;
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = logName });
					res = dbCmd.ExecuteNonQuery ();
					//Debug.Log ("del acc : " + res); //1 = success when using insert
				}

			} catch (Exception e) {
				//Debug.Log ("Error delete all ");
			} finally {
				conn.Close ();
			}
		}
	}

	public void deleteProfile ()
	{ 
		using (IDbConnection conn = new SqliteConnection (dbPath)) {
			try {
				conn.Open ();
				using (IDbCommand dbCmd = conn.CreateCommand ()) {
					//delete child all profiles first
					string query = "DELETE FROM Profile WHERE CharName = @chname";
					dbCmd.CommandText = query;
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "chname", Value = charname });

					int res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					//Debug.Log ("del prof only: " + res); //1 = success when using insert

					query = "UPDATE Account SET CharCount = @cc WHERE Username = @usname;";
					dbCmd.CommandText = query;
					charCount--;
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "cc", Value = charCount });
					dbCmd.Parameters.Add (new SqliteParameter { ParameterName = "usname", Value = logName });

					res = dbCmd.ExecuteNonQuery ();
					//object sd = dbCmd.ExecuteScalar ();
					//Debug.Log ("update account charcount: " + res); 
					//Debug.Log ("charcount: " + charCount);//1 = success when using insert
					charname = null;
					readProfile ();
				}

			} catch (Exception e) {
				//Debug.Log ("Error delete 1 prof ");
			} finally {
				conn.Close ();
			}
		}
	}
}


