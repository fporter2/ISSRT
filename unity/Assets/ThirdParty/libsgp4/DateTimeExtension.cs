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
using sgp4;


public static class DateTimeExtention
{
	/// <summary>
	/// Only for modern dates, converts to julian date
	/// see: http://stackoverflow.com/questions/5248827/convert-datetime-to-julian-date-in-c-sharp-tooadate-safe
	/// </summary>
	/// <returns>The julian date.</returns>
	/// <param name="date">Date.</param>
	public static double ToJulian(this DateTime date)
	{
		return date.ToOADate() + 2415018.5;
	}
	
	/**
     * Convert to greenwich sidereal time
     * @returns the greenwich sidereal time
     */
	public static double ToGreenwichSiderealTime(this DateTime date)
	{
		// t = Julian centuries from 2000 Jan. 1 12h UT1
		double t = (date.ToJulian() - 2451545.0) / 36525.0;
		
		// Rotation angle in arcseconds
		double theta = 67310.54841
			+ (876600.0 * 3600.0 + 8640184.812866) * t
				+ 0.093104 * t * t
				- 0.0000062 * t * t * t;
		
		// 360.0 / 86400.0 = 1.0 / 240.0
		return Util.WrapTwoPI(Util.DegreesToRadians(theta / 240.0));
	}

	/**
     * Convert to local mean sidereal time (GMST plus the observer's longitude)
     * @param[in] lon observers longitude
     * @returns the local mean sidereal time
     */
	public static double ToLocalMeanSiderealTime(this DateTime date, double lon)
	{
		return Util.WrapTwoPI(date.ToGreenwichSiderealTime() + lon);
	}
}