CREATE PROCEDURE SP_CreatePayments
    @CardNumber VARCHAR(15),
    @Amount DECIMAL(18,2),
    @TransactionId INT OUTPUT,
    @MensajeSalida VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentBalance DECIMAL(18,2);

    -- Verificar si la tarjeta existe y obtener saldo
    IF NOT EXISTS (SELECT 1 FROM Tarjetas WHERE CardNumber = @CardNumber)
    BEGIN
        SET @MensajeSalida = 'Tarjeta no encontrada.';
        RETURN;
    END

    SELECT @CurrentBalance = Balance FROM Tarjetas WHERE CardNumber = @CardNumber;

    -- Verificar si hay saldo suficiente
    IF @CurrentBalance < @Amount
    BEGIN
        SET @MensajeSalida = 'Saldo insuficiente.';
        RETURN;
    END

    -- Actualizar saldo de la tarjeta
    UPDATE Tarjetas
    SET Balance = Balance - @Amount
    WHERE CardNumber = @CardNumber;

    -- Insertar la transacción
    INSERT INTO Transacciones (CardNumber, Monto, Fecha)
    VALUES (@CardNumber, @Amount, GETDATE());

    SET @TransactionId = SCOPE_IDENTITY();
    SET @MensajeSalida = 'Pago realizado exitosamente.';
END;
