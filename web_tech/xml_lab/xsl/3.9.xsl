<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
    <xsl:template match="/">
        <H2>Описание книг</H2>
        <xsl:apply-templates select="INVENTORY/BOOK" />
    </xsl:template>
    <xsl:template match="BOOK">
        <SPAN STYLE="font-style:italic">Автор: </SPAN>
        <xsl:value-of select="AUTHOR"/><BR/>
        <SPAN STYLE="font-style:italic">Наименование: </SPAN>
        <xsl:value-of select="TITLE"/><BR/>
        <SPAN STYLE="font-style:italic">Цена: </SPAN>
        <xsl:value-of select="PRICE"/><BR/>
        <SPAN STYLE="font-style:italic">Число страниц: </SPAN>
        <xsl:value-of select="PAGES"/><BR/>
        <SPAN STYLE="font-style:italic">Тип переплета: </SPAN>
        <xsl:value-of select="BINDING"/><P/>


    </xsl:template>
</xsl:stylesheet>
