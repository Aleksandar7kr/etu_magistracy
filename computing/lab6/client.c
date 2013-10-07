#include <sys/types.h>
#include <sys/socket.h>
#include <sys/statvfs.h>
#include <netinet/in.h>

int main()
{
	int sock;
	struct sockaddr_in addr;
	struct statvfs statvfs_buf;
	unsigned long freespace;

	sock = socket(AF_INET, SOCK_STREAM, 0);
	if(sock < 0)
	{
		perror("socket");
		exit(1);
	}

	addr.sin_family = AF_INET;
	addr.sin_port = htons(666);
	addr.sin_addr.s_addr = htonl(INADDR_LOOPBACK);

	while(connect(sock, (struct sockaddr *) &addr, sizeof(addr)) < 0);

	while(1)
	{
		statvfs("/", &statvfs_buf);
		freespace = statvfs_buf.f_bsize * statvfs_buf.f_bfree;
		send(sock, &freespace, sizeof(freespace), 0);
		sleep(5);
	}

	return 0;
}