using UnityEngine;
using System;

public static class MBExtensions
{
	public static T InstantiateFromComponent<T>(this T src) where T: MonoBehaviour
	{
		T res = null ;
		GameObject obj = (GameObject)GameObject.Instantiate(src.gameObject);
		if(obj==null)
			throw new UnityException("GameObject.Instantiate returned null");

		res = obj.GetComponent<T>();

		if(res==null)
			throw new UnityException("Instantiated object does not contain component "+typeof(T).Name);

		return res;
	}


}

