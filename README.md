![Testura Logo](https://i.ibb.co/z7WTnp2/logo2.png)

Testura.Code is a wrapper around the Roslyn API and used for generation, saving and compiling C# code. It provides methods and helpers to generate classes, methods, statements and expressions.

It provide helpers to generate:

- Classes 
- Methods 
- Parameters 
- Arguments 
- Attributes
- Fields 
- Properties

But also simple statements like: 

- Declaration statements (for example declare and assign variables)
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

## Documentation 

- Wiki -[https://github.com/Testura/Testura.Code/wiki](https://github.com/Testura/Testura.Code/wiki)
- Api - [https://testura.github.io/Code/api/index.html](https://testura.github.io/Code/api/index.html)

## Examples 

### Hello world 

Here is an example on how to generate, save and compile a simple hello world. 

#### Generate

```c#
var @class = new ClassBuilder("Program", "HelloWorld")
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

//To a dll

// From string
var result = await compiler.CompileSourceAsync(@"/path/HelloWorld.dll", generatedCode);

// From file
var result = await compiler.CompileFilesAsync(@"/path/HelloWorld.dll",  @"/path/HelloWorld.cs");

//In memory (without creating a dll)

// From string
var result = await compiler.CompileSourceInMemoryAsync(generatedCode);

// From file
var result = await compiler.CompileFilesInMemoryAsync(@"/path/HelloWorld.cs");
```

## More advanced examples

### Model class

```c#
        var classBuilder = new ClassBuilder("Cat", "Models");
        var @class = classBuilder
            .WithUsings("System")
            .WithConstructor(
                ConstructorGenerator.Create(
                    "Cat",
                    BodyGenerator.Create(
                        Statement.Declaration.Assign("Name", ReferenceGenerator.Create(new VariableReference("name"))),
                        Statement.Declaration.Assign("Age", ReferenceGenerator.Create(new VariableReference("age")))),
                    new List<Parameter> { new Parameter("name", typeof(string)), new Parameter("age", typeof(int)) },
                    new List<Modifiers> { Modifiers.Public }))
            .WithProperties(
                PropertyGenerator.Create(new AutoProperty("Name", typeof(string), PropertyTypes.GetAndSet, new List<Modifiers> { Modifiers.Public })),
                PropertyGenerator.Create(new AutoProperty("Age", typeof(int), PropertyTypes.GetAndSet, new List<Modifiers> { Modifiers.Public })))
            .Build();
```

Will generate 

```c#
using System;

namespace Models
{
    public class Cat
    {
        public Cat(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; set; }
        public int Age { get; set; }
    }
}
```

### Enum 

```c#
        var @class = new ClassBuilder("Cat", "Models")
            .WithUsings("System")
            .With(new EnumBuildMember("MyEnum", new List<EnumMember> { new("EnumValueOne", 2, new Attribute[] { new Attribute("MyAttribute"), }), new EnumMember("EnumValueTwo") }, new List<Modifiers> { Modifiers.Public }))
            .Build();
```

Will generate

```c#
using System;

namespace Models
{
    public class Cat
    {
        public enum MyEnum
        {
            [MyAttribute]
            EnumValueOne = 2,
            EnumValueTwo
        }
    }
}
```

### Class with file scoped namespace and body properties

```c#
        var @class = new ClassBuilder("Cat", "Models", NamespaceType.FileScoped)
            .WithUsings("System")
            .WithFields(
                new Field("_name", typeof(string), new List<Modifiers>() { Modifiers.Private }),
                new Field("_age", typeof(int), new List<Modifiers>() { Modifiers.Private }))
            .WithConstructor(
                ConstructorGenerator.Create(
                    "Cat",
                    BodyGenerator.Create(
                        Statement.Declaration.Assign("Name", ReferenceGenerator.Create(new VariableReference("name"))),
                        Statement.Declaration.Assign("Age", ReferenceGenerator.Create(new VariableReference("age")))),
                    new List<Parameter> { new Parameter("name", typeof(string)), new Parameter("age", typeof(int)) },
                    new List<Modifiers> { Modifiers.Public }))
            .WithProperties(
                PropertyGenerator.Create(
                    new BodyProperty(
                        "Name",
                        typeof(string),
                        BodyGenerator.Create(Statement.Jump.Return(new VariableReference("_name"))), BodyGenerator.Create(Statement.Declaration.Assign("_name", new ValueKeywordReference())),
                        new List<Modifiers> { Modifiers.Public })),
                PropertyGenerator.Create(
                    new BodyProperty(
                        "Age",
                        typeof(int),
                        BodyGenerator.Create(Statement.Jump.Return(new VariableReference("_age"))), BodyGenerator.Create(Statement.Declaration.Assign("_age", new ValueKeywordReference())),
                        new List<Modifiers> { Modifiers.Public })))
            .Build();
```

Will generate

```c#
using System;

namespace Models;

public class Cat
{
    private string _name;
    private int _age;

    public Cat(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }

    public int Age
    {
        get
        {
            return _age;
        }

        set
        {
            _age = value;
        }
    }
}
```

### Record with primary constructor

```c#
        var record = new RecordBuilder("Cat", "Models", NamespaceType.FileScoped)
            .WithUsings("System")
            .WithPrimaryConstructor(new Parameter("Age", typeof(int)))
            .Build();
```

Will generate

```c#
using System;

namespace Models;

public record Cat(int Age);
```

### Class with methods that override operators and comments

```c#
        var @class = new ClassBuilder("Cat", "Models")
            .WithUsings("System")
            .WithMethods(new MethodBuilder("MyMethod")
                .WithModifiers(Modifiers.Public, Modifiers.Static)
                .WithOperatorOverloading(Operators.Increment)
                .WithParameters(new Parameter("MyParameter", typeof(string)))
                .WithBody(
                    BodyGenerator.Create(
                        Statement.Declaration.Declare("hello", typeof(int)).WithComment("My comment above").WithComment("hej"),
                        Statement.Declaration.Declare("hello", typeof(int)).WithComment("My comment to the side", CommentPosition.Right)))
                .Build())
            .Build();
```

Will generate

```c#
using System;

namespace Models
{
    public class Cat
    {
        public static MyMethod operator ++(string MyParameter)
        {
            //hej
            int hello;
            int hello; //My comment to the side
        }
    }
}
```

## Missing anything? 

If we miss a feature, syntax or statements - just create an issue or contact us and I'm sure we can add it.

It is also possible for you to contribute with your own feature. Simply add a pull request and we will look at it. 

## License

This project is licensed under the MIT License. See the [LICENSE.md](LICENSE.md) file for details.

## Contact

Visit <a href="http://www.testura.net">www.testura.net</a>, twitter at @testuranet or email at mille.bostrom@testura.net
