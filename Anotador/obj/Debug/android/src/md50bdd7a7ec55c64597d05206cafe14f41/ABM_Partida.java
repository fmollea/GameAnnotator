package md50bdd7a7ec55c64597d05206cafe14f41;


public class ABM_Partida
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Anotador.ABM_Partida, Anotador, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ABM_Partida.class, __md_methods);
	}


	public ABM_Partida ()
	{
		super ();
		if (getClass () == ABM_Partida.class)
			mono.android.TypeManager.Activate ("Anotador.ABM_Partida, Anotador, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
