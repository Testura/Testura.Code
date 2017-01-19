![Testura Logo](http://testura.net/Content/Images/logo.png)

Testura.Code is a wrapper around the Roslyn API and used for generation, saving and and compiling C# code. It provides methods and helpers to generate classes, methods, statements and expressions.

It provide helpers to generate:

- Classes 
- Methods 
- Parameters 
- Arguments 
- Attributes
- Fields 
- Properties

But also simple statements like: 

- Decleration statements (for example declare and assign variables)
- Iterations statements (for example for-loop)
- Jump statements (for example return)
- Selection statement (for example if-statements)
- Expression statements (for example invoke methods)


# Install

## NuGet [![NuGet Status](https://img.shields.io/nuget/v/Testura.Code.svg?style=flat)](https://www.nuget.org/packages/Testura.Code)

[https://www.nuget.org/packages/Testura.Code](https://www.nuget.org/packages/Testura.Code)
    
    PM> Install-Package Testura.Code

## Usage

Testura.Code have three different types of helpers: 

- `Generators` - The most basic kinds of code generators, for example fields, properties and modifiers. 
- `Statement` - Helpers for regular statements and expressions, for example declare and assign a variable or invoke a method. 
- `Builders` - Currently we have two builder - One class builder and one method builder. These have the highest  abstraction and are easy to use. 

For more details, check out the wiki: [https://github.com/Testura/Testura.Code/wiki](https://github.com/Testura/Testura.Code/wiki)

## Examples 

### Hello world 

Here is an example on how to generate, save and compile a simple hello world. 

#### Generate

```c#
var @class = new ClassBuilder("System", "HelloWorld")
	.WithUsings("System") 
	.WithModifiers(Modifiers.Public)
	.WithMethods(
		new MethodBuilder("Main")
		.WithModifiers(Modifiers.Public, Modifiers.Static)
		.WithParameters(new Parameter("args", typeof(string[])))
		.WithBody(
			BodyGenerator.Create(
				Statement.Expression.Invoke("Console", "WriteLine", new List<IArgument>() { new ValueArgument("Hello world") }).AsStatement(),
				Statement.Expression.Invoke("Console", "ReadLine").AsStatement()
				))
		.Build())
	.Build();
```
 
 This code will generate following code: 
 
 ```c#
using System;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Hello world");
            Console.ReadLine();
        }
    }
}
```

#### Save 

```c#
var saver = new CodeSaver();

// As a string
var generatedCode = saver.SaveCodeAsString(@class);

// Or to file
saver.SaveCodeToFile(@class, @"/path/HelloWorld.cs");
```

#### Compile

```c#
var compiler = new Compiler();

// From string
var result = await compiler.CompileSourceAsync(@"/path/HelloWorld.dll", code);

// From file
var result = await compiler.CompileSourceAsync(@"/path/HelloWorld.dll",  @"/path/HelloWorld.cs");
```

## Missing anything? 

If we miss a feature, syntax or statements - just create an issue or contact us and I'm sure we can add it.

It is also possible for you to contribute with your own feature. Simply add a pull request and we will look at it. 

## License

This project is licensed under the MIT License. See the [LICENSE.md](LICENSE.md) file for details.

## Contact

Visit <a href="http://www.testura.net">www.testura.net</a>
