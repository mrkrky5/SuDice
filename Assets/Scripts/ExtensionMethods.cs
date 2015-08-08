using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods
{
	public static int Mod (this int a, int b)
	{
		return (Mathf.Abs (a * b) + a) % b;
	}

	public static string ToIntString (this int value, int count)
	{
		var output = "";
		var valuestring = value.ToString ();
		var times = count - valuestring.Length;
		for (int i=0; i<times; i++) {
			output += "0";
		}
		output += valuestring;
		return output;
	}
}