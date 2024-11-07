## Requisitos Previos
- Visual Studio con herramientas de desarrollo ASP.NET y web

## Instrucciones
1. **Clonar repositorio**
2. **Seleccionar abrir proyecto o solución con Visual Studio y seleccionar `BankAccountManagementAPI.sln`**
3. **Iniciar la depuración HTTP**

## Probar Ejemplo de Uso

### Crear una Cuenta con Saldo Inicial
**Método:** POST `"/api/UserAccount"`

**Cuerpo del mensaje (Body):**
```json
{
  "name": "string",
  "middleName": "string",
  "lastName": "string",
  "secontLastName": "string",
  "inicialAmount": 0,
  "currency": 0
}
```

**Devolverá una respuesta con la información del usuario y el número de cuenta en llave “accountNumber”**

### Realizar un depósito
**Método:** POST `"/api/AccountTransaction/deposit"`

**Cuerpo del mensaje (Body):**
```json
{
  "amount": 0,
  "accountNumber": 0
}
```

### Realizar un retiro
**Método:** POST `"/api/AccountTransaction/withdraw"`

**Cuerpo del mensaje (Body):**
```json
{
  "amount": 0,
  "accountNumber": 0
}
```

### Realizar una consulta de saldo
**Método:** GET `"/api/UserAccount/balance/{accountNumber}"`

**accountNumber = número de Cuenta**

**Devolverá el saldo actual de la cuenta.**

### Obtener Resumen de transacciones
**Método:** GET `"/api/AccountTransaction/record/{accountNumber}"`

**accountNumber = número de Cuenta**

**Devolverá la información de la cuenta del usuario con su saldo actual y el resumen de transacciones.**

## Pruebas unitarias
**Las pruebas unitarias se realizaron con xUnit en el proyecto `BankAccountManagementAPITesting` y se pueden ejecutar con el explorador de pruebas de visual Studio (pestaña ver -> explorador de pruebas**

