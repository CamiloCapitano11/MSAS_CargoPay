CREATE PROCEDURE SP_GetCardsBalance
    @CardNumber VARCHAR(15),
    @Balance DECIMAL(18,2) OUTPUT,
    @MensajeSalida VARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Buscar el saldo de la tarjeta
    IF EXISTS (SELECT 1 FROM Tarjetas WHERE CardNumber = @CardNumber)
    BEGIN
        SELECT @Balance = Balance FROM Tarjetas WHERE CardNumber = @CardNumber;
        SET @MensajeSalida = 'Consulta exitosa.';
    END
    ELSE
    BEGIN
        SET @MensajeSalida = 'Tarjeta no encontrada.';
        SET @Balance = 0;
    END
END;
