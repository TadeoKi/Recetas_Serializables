# Universidad Católica del Uruguay
## FIT
### Programación II

Este código de ejemplo está basado en las recetas de cocina que involucran ingredientes y equipamiento.

Los objetos en este ejemplo pueden ser convertidos a una string en formato [Json](https://www.json.org/json-en.html) y pueden ser recreados desde una string en formato Json.

Convertir los objetos a y desde texto en formato Json es útil cuando queremos "transmitir" los objetos de un lugar a otro, porque podemos convertir los objetos a texto en formato Json en el origen, "transmitir" el texto -que suele ser más fácil que "transmitir" objetos-, y luego crear nuevamente los objetos a partir del texto en forma Json en el destino. Cuando decimos "transmitir" nos referimos por ejemplo a pasar el objeto de memoria a disco, o desde un servidor a otro.

Al proceso de convertir objetos a una representación que se puede transmitir se le llama **serialización**. Al proceso opuesto de crear objetos a partir de la representación transmitida se le llama **deserialización**.

El texto en formato Json se puede guardar en un archivo con `File.WriteAllText(string,string)` donde el primer argumento es el nombre del archivo y el segundo el texto a guardar. Luego ese texto puede ser leido con `File.ReadAllText(string)` donde el argumento es el nombre del archivo. De esta forma pueden persistir sus objetos entre sucesivas ejecuciones del programa. También pueden dar un estado inicial editando el contenido del archivo para que contenga los objetos que deseen en formato Json.

Las clases de los objetos que pueden ser convertidos a formato Json implementan la interfaz `IJsonConvertible` que tiene una operación `string ConvertToJson()`, que genera una representación del objeto en ese formato utilizando `string JsonSerializer.Serialize(object)`. Para convertir el texto en formato Json al objeto correspondiente se utiliza `T JsonSerializer.Deserialize<T>(string)` donde `T` es la clase del objeto oportunamente convertido a texto en ese formato. La clase `JsonSerializer` está definida en el espacio de nombres `String.Text.Json`.

El método `JsonSerializer.Serialize` va convertir a formato Json todas las propiedades públicas del objeto. El el caso que esa propiedad sea un objeto, va a convertir también las propiedades de ese otro objeto. En el caso de que una propiedad pública sea una colección y tenga el atributo `[JsonInclude]`, va a convertir también los objetos contenidos en esa colección. De esta forma es posible lograr convertir un objeto y todos los demás objetos referenciados o contenidos en él.

En este enfoque hay un problema cuando un objeto es referenciado desde más de un objeto, es decir, cuando es un objeto compartido: el método `Deserialize` de la clase `JsonSerializer` va a crear instancias diferentes del objeto compartido cada vez que se convierta uno de los objetos que lo comparten.

Para evitarlo vean la descripción de la solución [aquí](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-preserve-references?pivots=dotnet-5-0), que está implementada en este ejemplo.

:warning: **Limitaciones de la serialización**

- La serialización no incluye el tipo de los objetos serializados. Por lo tanto tienen que saber el tipo del objeto en el momento de deserializar.

- Sólo se serializan las propiedades públicas de las clases.

- Aunque una clase puede tener constructor con parámetros, también tiene que tener un constructor sin parámetros, que marcan con el atributo `[JsonConstructor]`.

- Cuando un objeto tiene una propiedad que es una colección, para que se serialize el contenido debe estar marcada con el atributo `[JsonInclude]`.

- Todos los objetos de una propiedad que es una colección que va a ser serializada deben ser de la misma clase. Por lo tanto la propiedad no puede estar declarada como una colección de una interfaz o de una clase abstracta.

:warning: El código no compila.

Deberán agregar los métodos que faltan para que el código compile y además lograr que pasen los casos de prueba. Para ello deberán utilizar los conceptos de serialización mostrados [aquí](https://github.com/ucudal/PII_Person_Serialization).
