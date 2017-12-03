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
    }
    public void Multiply(float multiplier) { //El resultado sobreescribe el vector sobre el que se haga el .Multiply
        x = x * multiplier;
        y = y * multiplier;
        z = z * multiplier;
    }
}

public class Our_Quaternion {
    public float w, x, y, z; //Componentes del quaternion
    public Our_Quaternion(Our_Vector3 axis, float angle) { //Al quaternion hay que pasarle el eje que obtenemos a hacer click en la pelota + el angulo de giro que queremos 
        w = Mathf.Cos(angle / 2);
        x = axis.x * Mathf.Sin(angle / 2);
        y = axis.y * Mathf.Sin(angle / 2);
        z = axis.z * Mathf.Sin(angle / 2);
    }
    //Creo que no nesecitamos metodos porque no operamos con quaternions, solo aplicamos a la rotacion del objeto  un quaternion, no sumamos quaternions entre ellos ni nada.
}