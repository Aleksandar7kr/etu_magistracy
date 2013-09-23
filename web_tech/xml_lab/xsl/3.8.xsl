<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
    <xsl:template match="/">
        <H2>�������� ����</H2>
        <xsl:for-each select="INVENTORY/BOOK">
            <SPAN STYLE="font-style:italic">Title: </SPAN>
            <xsl:value-of select="TITLE"/><BR />
            <xsl:for-each select="AUTHOR">
                <SPAN STYLE="font-style:italic">�����: </SPAN><BR/>
                <SPAN STYLE="color:green">�������: </SPAN>
                <xsl:value-of select="LASTNAME"/><BR />
                <SPAN STYLE="color:green">���: </SPAN>
                <xsl:value-of select="FIRSTNAME"/><BR />

            </xsl:for-each>
            <SPAN STYLE="font-style:italic">��� ���������: </SPAN>
            <xsl:value-of select="BINDING"/><BR />
            <SPAN STYLE="font-style:italic">����� �������: </SPAN>
            <xsl:value-of select="PAGES"/><BR />
            <SPAN STYLE="font-style:italic">����: </SPAN>
            <xsl:value-of select="PRICE"/><P />
        </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>
