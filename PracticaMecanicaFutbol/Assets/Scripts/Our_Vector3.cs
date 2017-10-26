using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Our_Vector3 : MonoBehaviour {

	private float X, Y, Z;
    public void setVariables(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    //Poder acceder a cada una de las componentes del vector( Velocity.x...)
    public float getX(){
		return X;
	}
	public float getY(){
		return Y;
	}
	public float getZ(){
		return Z;
	}
	public Our_Vector3 CrossProduct(Our_Vector3 vector_b){
		Our_Vector3 temp = new Our_Vector3();
        //TO DO
		return temp;
	}
	public float DotProduct(Our_Vector3 vector_b){
		return (X * vector_b.X) + (Y * vector_b.Y) + (Z * vector_b.Z);
	}
	public void Normalize(Our_Vector3 vector_b){
        //TO DO
	}
	public float Module(){
		return Mathf.Sqrt((Mathf.Pow(X,2))+(Mathf.Pow(Y,2))+(Mathf.Pow(Z,2)));
	}
	public void Add(Our_Vector3 vector_b){
		X = X + vector_b.getX ();
		Y = Y + vector_b.getY ();
		Z = Z + vector_b.getZ ();
	}
	public void Substract(Our_Vector3 vector_b){
		X = X - vector_b.getX ();
		Y = Y - vector_b.getY ();
		Z = Z - vector_b.getZ ();
	} 
    //Poder dividir un vector 3 entre float
    //Poder multiplicar un vector 3 por float
}