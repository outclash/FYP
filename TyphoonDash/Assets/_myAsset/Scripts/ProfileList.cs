using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class that will hold the profile values of an account
*/
public class ProfileList {

	public string CharName {get;set;}
	public string Score {get;set;}
	public int Coins {get;set;}
	public int PU1 {get;set;}
	public int PU2 {get;set;}

	public ProfileList(string cname, string sc, int coin, int pu1, int pu2){
		CharName = cname;
		Score = sc;
		Coins = coin;
		PU1 = pu1;
		PU2 = pu2;
	}
}
