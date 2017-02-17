/* Name: txt2html.exe
Purpose: To read in a text file of some sort and turn it into an HTML file.
Coder: Justin Dawid
Due Date: March 18, 2013
*/

#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <locale>
/*
//use these for memory leaks:
#define _CRTDBG_MAP_ALLOC

#include <crtdbg.h>;*/

using namespace std;

/* Name: fail()
Purpose: To tell the user how to properly input parameters into the console to work the program.
Accepts: N/A
Returns: N/A
*/
void fail()
{
	cout << "Error: Incorrect parameters.\n";
	cout << "Usage: txt2html [-q] sourcefile destfile\n\n";
}

/* Name: findSwitch()
Purpose: To find the '-q' switch in the input list, if it exists
Accepts: vector<string> s - the vector of inputs where the switch may be
Returns: int switchAt - where the switch is located (0 if not found)
*/
int findSwitch(vector<string> s)
{
	int switchAt = 0;
	for(unsigned i = 1; i < s.size(); ++i)
		if(s[i] == "-q")
			switchAt = i;
	return switchAt;
}

/* Name: createTitle()
Purpose: To create a title for the HTML document
Accepts: string titleToBe - the file name of the input file
Returns: string titleToBe - input file's name without its extension
*/
string createTitle(string titleToBe)
{
	unsigned found = titleToBe.rfind(".");
	if (found != string::npos)
		titleToBe.resize (found);
	return titleToBe;
}

/* Name: appendExt()
Purpose: To append the .html extension onto an output file name
Accepts: string s - the output file name
Returns: string s - the output file name with .html appended
*/
string appendExt(string s)
{
	return s.append(".html");
}

/* - - - - - MAIN - - - - - */
int main(int argc, char* argv[])
{/*
	//start memory leak debugging flags
#if defined(_DEBUG)
	int dbgFlags = ::_CrtSetDbgFlag(_CRTDBG_REPORT_FLAG);
	//bitwise or check block integrity on every memory call
	dbgFlags |= _CRTDBG_CHECK_ALWAYS_DF;
	//don't always remove blocks on delete
	dbgFlags |= _CRTDBG_DELAY_FREE_MEM_DF;
	//check for memory leaks @ process termination
	dbgFlags |= _CRTDBG_LEAK_CHECK_DF;
	//use modified dbgflags
	_CrtSetDbgFlag(dbgFlags);
#endif

	locale here("");
	cout.imbue(here);
	*/
	//shove the command line inputs onto a vector to make them easier to access
	vector<string> s;
	for(int i=0; i < argc; ++i)
		s.push_back(argv[i]);

	//make sense of which piece of input is which
	int switchAt = findSwitch(s);
	string inFileName;
	string outFileName;

	if(s.size() == 4) //path to .exe, infile name, switch and outfile name
	{
		if(switchAt == 0)//one element of the input must be unreadable garbage
		{
			fail();
			return EXIT_FAILURE;
		}
		(switchAt == 1) ? inFileName = s[2] : inFileName = s[1];
		(switchAt == 3) ? outFileName = s[2] : outFileName = s[3];
	}
	else if(s.size() == 3) //path to .exe, infile name, 1 of (switch, outfile name)
	{
		if(switchAt == 0)
		{
			inFileName = s[1];
			outFileName = s[2];
		}
		else
			(switchAt == 1) ? inFileName = s[2] : inFileName = s[1];
	}
	else if(s.size() == 2) //path to .exe, infile name
		inFileName = s[1];
	else //anything else has incorrect parameters
	{
		fail();
		return EXIT_FAILURE;
	}
	
	//create title for HTML file (and potentially outfile name)
	string title = createTitle(inFileName);
	if(outFileName == "")
		outFileName = appendExt(title);

	//initialize streams
	ifstream in(inFileName);
	if(!in)
	{
		cout << "Could not open the input file\n";
		return EXIT_FAILURE;
	}
	//in.imbue(here);

	ofstream out(outFileName);
	if(!out)
	{
		cout << "Could not open the output file\n";
		return EXIT_FAILURE;
	}
	//out.imbue(here);

	//read infile and write outfile
	string txtLine;
	int lines = 0;
	int paragraphs = 0;

	out << 
	"<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\">\n" << "<head>\n" <<
	"<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\n" <<
	"<title>" << title << "</title>\n" << "</head>\n<body>";
	while(getline(in, txtLine))
	{
		++lines;
		int r = txtLine.length();
		out << endl;
		if(r==0)
		{
			out << "<br />";
			++paragraphs;
		}
		out << txtLine;
	}
	out << "\n</body>\n</html>";
	
	//output counts unless asked not to
	if(switchAt == 0)
	{
		cout << "Input lines processed: " << lines << endl;
		cout << "Output paragraphs processed: " << paragraphs << endl;
	}
	
	//memory leak checking goes @ end
	//_CrtDumpMemoryLeaks();
}