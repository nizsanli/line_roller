using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {
	
	public abstract void activate();
	public abstract void init(Player player);

}
