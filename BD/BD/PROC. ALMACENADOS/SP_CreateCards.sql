CREATE PROCEDURE SP_CreateCards
    @CardNumber VARCHAR(15),
    @InitialBalance DECIMAL(18,2),
    @MensajeSalida VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si la tarjeta ya existe
    IF EXISTS (SELECT 1 FROM Tarjetas WHERE CardNumber = @CardNumber)
    BEGIN
        SET @MensajeSalida = 'La tarjeta ya existe.';
        RETURN;
    END

    -- Insertar la nueva tarjeta
    INSERT INTO Tarjetas (CardNumber, Balance, FechaCreacion)
    VALUES (@CardNumber, @InitialBalance, GETDATE());

    SET @MensajeSalida = 'Tarjeta creada exitosamente.';
END;
