<?xml version="1.0" encoding="windows-1251"?>
<!-- XslDemo01.xsl -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
    <xsl:template match="/">
        <H2>Book Description</H2>
        <SPAN STYLE="font-style:italic">Author: </SPAN>
        <xsl:value-of select="BOOK/AUTHOR"/><BR/>
        <SPAN STYLE="font-style:italic">Title: </SPAN>
        <xsl:value-of select="BOOK/TITLE"/><BR/>
        <SPAN STYLE="font-style:italic">Price: </SPAN>
        <xsl:value-of select="BOOK/PRICE"/><BR/>
        <SPAN STYLE="font-style:italic">Binding type: </SPAN>
        <xsl:value-of select="BOOK/BINDING"/><BR/>
        <SPAN STYLE="font-style:italic">Number of pages: </SPAN>
        <xsl:value-of select="BOOK/PAGES"/>
    </xsl:template>
</xsl:stylesheet>
