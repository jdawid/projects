#include <iostream>
#include <memory>
#include <string>
#include <ctime>
using namespace std;

class Animal {
public:
	typedef shared_ptr<Animal> pointer_type;
private:
	string name_;
public:
	Animal() : name_("Animal") { }
	virtual ~Animal(){}	//always code if there are any virtual methods!!!
	virtual string get_name() const {return name_;}

};
class Canis : public Animal{

};

class Felis : public Animal{

};

class Domesticus : public Felis{
 string name_;
public:
 Domesticus() : name_("Domesticus") {}
 string get_name() const {return name_;}
};

class Persian : public Felis{
 string name_;
public:
 Persian() : name_("Persian") {}
 string get_name() const {return name_;}
};
class Dog : public Canis{ 
 string name_;
public:
 Dog() : name_("Dog") {}
 string get_name() const {return name_;}
};
class BullDog : public Canis{ 
 string name_;
public:
 BullDog() : name_("BullDog") {}
 string get_name() const {return name_;}
};
class Wolf : public Canis{
 string name_;
public:
 Wolf() : name_("Wolf") {}
 string get_name() const {return name_;}
};


Animal::pointer_type animal_factory() {
 switch( rand() % 3) {
 case 0 : return Animal::pointer_type(new Wolf);
 case 1 : return Animal::pointer_type(new Persian);
 case 2 : return Animal::pointer_type(new BullDog);
 default: return Animal::pointer_type(new Animal);
 }

}

ostream& operator << (ostream& os, Animal const& animal) //pass by ref is slurping
{
	return os << animal.get_name();
}

//ostream& operator << (ostream& os, Animal const animal) //pass by value is slicing
//{
//	return os << animal.get_name();
//}

int main(){
 srand( (unsigned) time(0) );
 for(int i = 0; i <10; i++) {
  Animal::pointer_type ptr = animal_factory();
  cout << *ptr << endl;
 }
}