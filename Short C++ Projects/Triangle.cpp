/***************************
Name: Triangle.cpp
Purpose: To take in the lengths and unit measurements of two sides of a
		right-angled triangle and to calculate the hypotenuse and two
		remaining angles, while using appropriate units
Coder: Justin Dawid
Due Date: Jan. 25, 2013
***************************/
#include <iostream>
#include <string>
using namespace std;

const double PI = acos(-1);		//the mathematical constant pi
void inputChecking(string,double&,string);		//method prototype for inputChecking

/* Name: calcHypotenuse(double, double)
Purpose: to calculate the hypotenuse of a triangle
Accepts: [double] x, y - the side lengths of the triangle
Returns: [double] - the length of the hypotenuse
*/
double calcHypotenuse(double x, double y)
{
	return sqrt(x*x + y*y);
}

/* Name: tangentCalcAngle(double, double)
Purpose: to calculate an angle of the triangle through use of 
		the tangent trigonometric ratio
Accepts: [double] opp, adj - the two side lengths
Returns: [double] - the angle measurement in degrees
*/
double tangentCalcAngle(double opp, double adj)
{
	return 180.0 * atan(opp/adj) / PI;
}

/* Name: areUnitsBad(string)
Purpose: to check if the units of measurement are usable; if not, the user is told so
Accepts: [string] units - the units of measurement input by the user
Returns: [bool] - true: units are unusable; false: units are good
*/
bool areUnitsBad(string units)
{
	if(units == "mm" || units == "cm" || units == "m" || units == "km")
		return false;
	else
	{
		cout << "For units, you must use one of: mm, cm, m, km" << endl;
		return true;
	}
}

/* Name: toMetres(double&, string)
Purpose: to 'remove' the metric prefix of a measurement
Accepts: [double&] side - the length of a side of a triangle
		[string] unit - the unit of measurement of the side
Returns: nothing - edited variables are passed by reference
*/
void toMetres(double& side, string unit)
{
	if(unit == "mm")
		side /= 1000.0;
	else if(unit == "cm")
		side /= 100.0;
	else if(unit == "km")
		side *= 1000.0;
}

/* Name: hypoUnits(double)
Purpose: to choose the most appropriate units for the hypotenuse and to convert
		it to those units from metres (if necessary).
Accepts: [double] h - the length of the hypotenuse
Returns: [string] - the units that are most appropriate for the hypotenuse
*/
string hypoUnits(double h)
{
	if(h < 0.01)
	{
		h *= 1000;
		return "mm";
	}
	else if(h < 1)
	{
		h *= 100;
		return "cm";
	}
	else if(h >= 1000)
	{
		h /= 1000;
		return "km";
	}
	else
		return "m";
}

//***MAIN METHOD***
int main()
{
	//get side lengths from user...
	double side1 = -1.0;
	double side2 = -1.0;
	bool badUnits = true;
	string unitString1;
	string unitString2;
	cout << "Enter the lengths and measurement units of the two sides adjacent\nto the right angle.\n";
	//...making sure that the input is usable:
	inputChecking("x = ", side1, unitString1);
	inputChecking("y = ", side2, unitString2);

	//calculate the hypotenuse, its proper units and the angles:
	double hypotenuse = calcHypotenuse(side1, side2);
	string finalUnits = hypoUnits(hypotenuse);
	double angle1 = tangentCalcAngle(side1, side2);
	double angle2 = tangentCalcAngle(side2, side1);
	//output program results:
	cout << "Hypotenuse: " << hypotenuse << finalUnits << "\nAngles: " << angle1 << ", " << angle2 << endl;

	return 0;
} //end main

/* Name: inputChecking(string, double&, string)
Purpose: To check if the user put in the side lengths and units properly
		and to convert the measurements to metres.
Accepts: [string] prompt - the string that asks the user for a side length
		[double&] side - the length of a triangle's side
		[string] units - the unit that the side length is measured in
Returns: nothing; value is passed in by reference
*/
void inputChecking(string prompt, double& side, string units)
{
	bool badUnits = true;
	do {
		cout << prompt;
		//clear out the input buffer from bad input
		cin.clear();
		cin.sync();
		cin >> side;
		if(side < 0)
			cout << "Side lengths must be positive values." << endl;
		cin >> units;
		badUnits = areUnitsBad(units);
	} while(side < 0 || badUnits);
	toMetres(side, units);
}