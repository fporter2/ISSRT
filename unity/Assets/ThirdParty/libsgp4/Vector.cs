/*
 * Copyright 2013 Daniel Warner <contact@danrw.com>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Text;

namespace sgp4 {

/**
 * @brief Generic vector
 *
 * Stores x, y, z, w
 */
class Vector4d
{

    /**
     * Default constructor
     */
	public Vector4d()
    {
		x=y=z=w=0;
    }

    /**
     * Constructor
     * @param arg_x x value
     * @param arg_y y value
     * @param arg_z z value
     */
    public Vector4d(double arg_x,
            double arg_y,
            double arg_z)
	{
			x=arg_x;
			y=arg_y;
			z=arg_z;
			w=0.0;
    }

    /**
     * Constructor
     * @param arg_x x value
     * @param arg_y y value
     * @param arg_z z value
     * @param arg_w w value
     */
	public Vector4d(double arg_x,
            double arg_y,
            double arg_z,
            double arg_w)
	{
			x=arg_x;
			y=arg_y;
			z=arg_z;
			w=arg_w;
    }
    
    /**
     * Copy constructor
     * @param v value to copy from
     */
	public Vector4d(Vector4d v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
        w = v.w;
    }

    /**
     * Assignment operator
     * @param v value to copy from
     */
	/*public  Vector operator=(Vector v)
    {
        if (this != &v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
            w = v.w;
        }
        return *this;
    }*/

    /**
     * Subtract operator
     * @param v value to suctract from
     */
	/*public Vector operator-(const Vector& v)
    {
        return Vector(x - v.x,
                y - v.y,
                z - v.z,
                0.0);
    }*/
	public static Vector4d operator -(Vector4d c1, Vector4d c2) 
	{
		return new Vector4d(c1.x - c2.x, c1.y - c2.y, c1.z - c2.z, 0.0);
	}

	public static Vector4d operator *(Vector4d c1, float c2) 
	{
		return new Vector4d(c1.x * c2, c1.y * c2, c1.z * c2, 0.0);
	}

    /**
     * Calculates the magnitude of the vector
     * @returns magnitude of the vector
     */
    public double Magnitude
    {
		get { return Math.Sqrt (x * x + y * y + z * z); }
    }

    /**
     * Calculates the dot product
     * @returns dot product
     */
    public double Dot(Vector4d vec)
    {
        return (x * vec.x) +
            (y * vec.y) +
            (z * vec.z);
    }

    /**
     * Converts this vector to a string
     * @returns this vector as a string
     */
    public override string ToString()
	{
        /*std::stringstream ss;
        ss << std::right << std::fixed << std::setprecision(3);
        ss << "X: " << std::setw(9) << x;
        ss << ", Y: " << std::setw(9) << y;
        ss << ", Z: " << std::setw(9) << z;
        ss << ", W: " << std::setw(9) << w;
        return ss.str();*/
		return string.Format ("{0} {1} {2} {3}", x, y, z, w);
    }

    /** x value */
    public double x;
    /** y value */
    public double y;
    /** z value */
    public double z;
    /** w value */
    public double w;
};

}