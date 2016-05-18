/* Simple behavior tree as our first prolog program
   Tree came from the Behavior Trees lecture
*/


% Behavioral Treee itself

%root :- lookForCat.				
root :- checkHunger,
		moveToFood,
		lookForCatFood.
root :-	moveToBed,
		lookForCatBed.
		
lookForCat:- checkCat,			
			  findCat,
			  checkChase,
			  chase.	

lookForCatFood :- checkCat,			
			  findCat,
			  checkChase,
			  chase.
lookForCatFood :- tooHungry,
				eatFood.

lookForCatBed :- checkCat,			
			  findCat,
			  checkChase,
			  chase.
lookForCatBed :- tooTired,
				sleepInBed.					

checkCat :- maybe(0.3).
checkHunger :- maybe(0.5).
checkChase :- maybe(0.4).
				
moveToFood :- write('The dog moves closer to the food.'), nl.
moveToBed :- write('The dog moves closer to the bed.'), nl.
findCat :- write('The cat is within site!'), nl.
chase :- write('The dog chases the cat'), nl.
tooHungry :- write('The dog is very hungry'), nl.
eatFood :- write('The dog eats the food'), nl.
tooTired :- write('The dog is very tired'), nl.
sleepInBed :- write('The dog sleeps'), nl.