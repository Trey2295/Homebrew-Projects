/*
 * C Program to Display the ATM Transaction
 */
#include <stdio.h>
#include <stdlib.h>
#include "OtherFile.c"
#include "OtherOtherFile.c"

int main() {    

    int number1, number2, answer;
    char str[50];
    
    printf("Enter two integers: ");
    scanf("%d %d", &number1, &number2);

    // calculating sum


    answer = addTheNumbers(number1, number2);    
    
    printf("%d + %d = %d", number1, number2, answer);

    printf("\n\nEnter some bullshit: ");
    scanf("%s", str);
    displayTest(str);
    return 0;
}
