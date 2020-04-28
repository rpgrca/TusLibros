# Fakes
En este directorio se encuentran las clases que simularán comportamiento. De vital importancia son los Builders que permiten crear objetos Stub con respuestas predefinidas utilizando sintáxis fluída.

La clase TusLibrosRestAPIStubBuilder permite crear una clase para empezar la prueba sin problemas: todas los objetos que se deberían inyectar al constructor son resueltos por el Builder. En caso de ser necesario permite configurar dichos objetos enviando como argumento un Builder configurado del objeto a utilizar.
