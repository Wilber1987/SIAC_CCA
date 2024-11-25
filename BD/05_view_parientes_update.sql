CREATE VIEW ViewParientesUpdate
AS
    SELECT fam.Idtfamilia, pariente.* FROM update_data.parientes_data_update pariente
    INNER JOIN familias fam ON fam.id = pariente.id_familia
;
