#include <iostream>
using namespace std;

int main()
{
	double totalWeight, mgOfTHC, singleMGofTHC, specificWeight, yourTHClevel, mgTHCyouwant, howMuchYouShouldTake;
	char answer;
	bool keepGoing = true;

	cout << "Enter total weight: ";
	cin >> totalWeight;
	cout << "Enter mg of THC: ";
	cin >> mgOfTHC;

	singleMGofTHC = totalWeight / mgOfTHC;

	system("cls");

	while (keepGoing)
	{
		cout << "Do you want to determine how much THC is in a specific weight?\n Enter Y if so ";
		cin >> answer;

		if ((answer == 'Y') || (answer == 'y'))
		{
			system("cls");
			cout << "Enter weight: ";
			cin >> specificWeight;

			yourTHClevel = specificWeight / singleMGofTHC;

			cout << "\n\nThat will be " << yourTHClevel << " of THC\n\n";

		}
		else
		{
			system("cls");
			cout << "Enter desired mg of THC you want: ";
			cin >> mgTHCyouwant;

			howMuchYouShouldTake = mgTHCyouwant * singleMGofTHC;

			cout << "\n\nYou need to take " << howMuchYouShouldTake << " grams to get that\n\n";
		}

	}

}