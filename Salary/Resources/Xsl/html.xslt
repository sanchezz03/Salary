<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<html>
			<head>
				<title>Scientists</title>
			</head>
			<body>
				<h1>Scientists</h1>
				<table border="1">
					<tr>
						<th>ID</th>
						<th>Type</th>
						<th>Full Name</th>
						<th>Faculty</th>
						<th>Department</th>
						<th>Position</th>
						<th>Salary</th>
						<th>Years on Position</th>
					</tr>
					<xsl:for-each select="Scientists/Scientist">
						<tr>
							<td>
								<xsl:value-of select="@id" />
							</td>
							<td>
								<xsl:value-of select="@type" />
							</td>
							<td>
								<xsl:value-of select="FullName" />
							</td>
							<td>
								<xsl:value-of select="Faculty" />
							</td>
							<td>
								<xsl:value-of select="Department" />
							</td>
							<td>
								<xsl:value-of select="Position" />
							</td>
							<td>
								<xsl:value-of select="Salary" />
							</td>
							<td>
								<xsl:value-of select="YearsOnPosition" />
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
