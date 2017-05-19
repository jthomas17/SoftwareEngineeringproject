package md5f6a8d907d22e5f6f172573dcfc190056;


public class SimpleImageScalerTask
	extends android.os.AsyncTask
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_doInBackground:([Ljava/lang/Object;)Ljava/lang/Object;:GetDoInBackground_arrayLjava_lang_Object_Handler\n" +
			"n_onPostExecute:(Ljava/lang/Object;)V:GetOnPostExecute_Ljava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("Swaha_for_Android.SimpleImageScalerTask, Swaha for Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SimpleImageScalerTask.class, __md_methods);
	}


	public SimpleImageScalerTask () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SimpleImageScalerTask.class)
			mono.android.TypeManager.Activate ("Swaha_for_Android.SimpleImageScalerTask, Swaha for Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public SimpleImageScalerTask (android.widget.ImageView p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == SimpleImageScalerTask.class)
			mono.android.TypeManager.Activate ("Swaha_for_Android.SimpleImageScalerTask, Swaha for Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Widget.ImageView, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public java.lang.Object doInBackground (java.lang.Object[] p0)
	{
		return n_doInBackground (p0);
	}

	private native java.lang.Object n_doInBackground (java.lang.Object[] p0);


	public void onPostExecute (java.lang.Object p0)
	{
		n_onPostExecute (p0);
	}

	private native void n_onPostExecute (java.lang.Object p0);

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
