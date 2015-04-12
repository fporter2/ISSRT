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

namespace sgp4 {
/**
 * @brief Stores a geodetic location (latitude, longitude, altitude).
 *
 * Internally the values are stored in radians and kilometres.
 */
class CoordGeodetic
{
	/**
     * Default constructor
     */
	public CoordGeodetic()
	{
		latitude = longitude = altitude = 0.0;
	}
	
	/**
     * Constructor
     * @param[in] lat the latitude (degrees by default)
     * @param[in] lon the longitude (degrees by default)
     * @param[in] alt the altitude in kilometers
     * @param[in] is_radians whether the latitude/longitude is in radians
     */
	public CoordGeodetic(
		double lat,
		double lon,
		double alt,
		bool is_radians = false)
	{
		if (is_radians)
		{
			latitude = lat;
			longitude = lon;
		}
		else
		{
			latitude = Util.DegreesToRadians(lat);
			longitude = Util.DegreesToRadians(lon);
		}
		altitude = alt;
	}
	
	/**
     * Copy constructor
     * @param[in] geo object to copy from
     */
	public CoordGeodetic(CoordGeodetic geo)
	{
		latitude = geo.latitude;
		longitude = geo.longitude;
		altitude = geo.altitude;
	}
	
	/**
     * Assignment operator
     * @param[in] geo object to copy from
     */
	/*CoordGeodetic& operator=(const CoordGeodetic& geo)
	{
		if (this != &geo)
		{
			latitude = geo.latitude;
			longitude = geo.longitude;
			altitude = geo.altitude;
		}
		return *this;
	}*/
	
	/**
     * Equality operator
     * @param[in] geo the object to compare with
     * @returns whether the object is equal
     */
	/*bool operator==(const CoordGeodetic& geo) const
	{
		return IsEqual(geo);
	}*/
	public bool Equals(CoordGeodetic geo)
	{
		return (latitude == geo.latitude &&
			longitude == geo.longitude &&
			altitude == geo.altitude);
	}
		
	/**
     * Inequality operator
     * @param[in] geo the object to compare with
     * @returns whether the object is not equal
     */
	/*bool operator!=(const CoordGeodetic& geo) const
	{
		return !IsEqual(geo);
	}*/
	
	/**
     * Dump this object to a string
     * @returns string
     */
	public override string ToString()
	{
		/*std::stringstream ss;
		ss << std::right << std::fixed << std::setprecision(3);
		ss << "Lat: " << std::setw(7) << Util::RadiansToDegrees(latitude);
		ss << ", Lon: " << std::setw(7) << Util::RadiansToDegrees(longitude);
		ss << ", Alt: " << std::setw(9) << altitude;
		return ss.str();*/
		//throw new NotImplementedException ();
		return string.Format ("{0} {1} {2}", Util.RadiansToDegrees (latitude), Util.RadiansToDegrees (longitude), altitude);
	}
	
	/** latitude in radians (-PI >= latitude < PI) */
	public double latitude;
	/** latitude in radians (-PI/2 >= latitude <= PI/2) */
	public double longitude;
	/** altitude in kilometers */
	public double altitude;
};

}