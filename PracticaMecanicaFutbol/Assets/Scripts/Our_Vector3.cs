using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Our_Vector3 {

	public float x, y, z;
    public float module;
    public Our_Vector3(float _x, float _y, float _z) { //Constructor -> hay que pasarle los 3 componentes.
        //NO puedes inicializar un Our_Vector3 vacio, sin pasarle ningun valor.
        x = _x;
        y = _y;
        z = _z;
        module = 0;
    }
    // User-defined conversion from Digit to double
    public static implicit operator Our_Vector3(Vector3 d)
    {
        return d;
    }
    //  User-defined conversion from double to Digit
    public static implicit operator Vector3(Our_Vector3 d)
    {
        return new Vector3(d.x, d.y, d.z);
    }
    public Our_Vector3 CrossProduct(Our_Vector3 vector_b){ //Devuelve un vector nuevo sin sobreescribir ningun resultado
		Our_Vector3 res = new Our_Vector3(y*vector_b.z - z*vector_b.y,z*vector_b.x - x*vector_b.z,x*vector_b.y - y*vector_b.x);
		return res;
	}
	public float DotProduct(Our_Vector3 vector_b){
		return (x * vector_b.x) + (y * vector_b.y) + (z * vector_b.z);
	}
	public void Normalize(){ //El resultado sobreescribe el vector sobre el que se hace el.Normalize
        float temp_VectorModule = this.Module();
        x = x / temp_VectorModule;
        y = y / temp_VectorModule;
        z = z / temp_VectorModule;
	}  
	public float Module(){ 
		return Mathf.Sqrt((Mathf.Pow(x,2))+(Mathf.Pow(y,2))+(Mathf.Pow(z,2)));
	}
	public void Add(Our_Vector3 vector_b){ //El resultado sobreescribe el vector sobre el que se hace el .Add
        x = x + vector_b.x;
		y = y + vector_b.y;
		z = z + vector_b.z;
        //return new Our_Vector3(x, y, z);
	}
	public void Substract(Our_Vector3 vector_b){ //El resultado sobreescribe el vector sobre el que se hace el .Substract
		x = x - vector_b.x;
		y = y - vector_b.y;
		z = z - vector_b.z;
	}
    public void Divide(float divider) { //El resultado sobreescribe el vector sobre el que se hace el .Divide
        x = x / divider;
        y = y / divider;
        z = z / divider;
        //return new Our_Vector3(x, y, z);
    }
    public void Multiply(float multiplier) { //El resultado sobreescribe el vector sobre el que se haga el .Multiply
        x = x * multiplier;
        y = y * multiplier;
        z = z * multiplier;
        //return new Our_Vector3(x, y, z);
    }
}

public class Our_Quaternion {
    public float w, x, y, z; //Componentes del quaternion
    public Our_Quaternion(float _x, float _y, float _z, float _w)
    {
        x = _x;
        y = _y;
        z = _z;
        w = _w;
    }
    public Our_Quaternion(float angle, Our_Vector3 axis) { //Al quaternion hay que pasarle el eje que obtenemos a hacer click en la pelota + el angulo de giro que queremos 
        axis.Normalize();
        w = Mathf.Cos((angle * Mathf.Deg2Rad) / 2);
        x = axis.x * Mathf.Sin((angle * Mathf.Deg2Rad) / 2);
        y = axis.y * Mathf.Sin((angle * Mathf.Deg2Rad) / 2);
        z = axis.z * Mathf.Sin((angle * Mathf.Deg2Rad) / 2);
    }


    public void Multiply(Our_Quaternion b)
    {
        float tempW, tempX, tempY, tempZ;     
        tempW = (w * b.w) - (x * b.x) - (y * b.y) - (z * b.z);  // w
        tempX = (w* b.x) + (x * b.w) - (y * b.z) + (z * b.y);  // x
        tempY = (w* b.y) + (x * b.y) + (y * b.w) - (z * b.x);  // y
        tempZ = (w* b.z) - (x * b.y) + (y * b.x) + (z * b.w);  // z
        w = tempW;
        x = tempX;
        y = tempY;
        z = tempZ;
    }
}